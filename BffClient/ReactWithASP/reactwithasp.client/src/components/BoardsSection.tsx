import { Link } from "react-router-dom";
import { Modal } from "./UI/Modal/Modal";
import { useMemo, useState } from "react";
import { Board, SectionType } from "../types/types";
import { IoClose, IoInformationCircleOutline, IoStarOutline } from "react-icons/io5";
import { useCreateBoardMutation, useUpdateBoardMutation } from "../redux/slices/apiSlice";

interface IBoardsSectionProps {
    boards: Board[],
    type: SectionType,
}

interface ISectionItemProps {
    board: Partial<Board>,
}

const getNewBoardInitialState = (): Partial<Board> => ({ title: "", isPrivate: false })

const filterBoards = (boards: Board[], type: SectionType): Board[] => {
    switch (type) {
        case SectionType.Favorite:
            return boards.filter(b => b.isFavorite);
        default:
            return boards;
    }
};

const SectionItem: React.FC<ISectionItemProps> = ({ board }) => {
    const { id, isFavorite, title } = board;
    const [UpdateBoardMutation] = useUpdateBoardMutation();

    const handleUpdateBoardFavoriteMutation = async () => {
        try {
            await UpdateBoardMutation({ id, isFavorite: !isFavorite });
        }
        catch (e) {
            console.error(e);
        }
    }

    return (
        <div className="board-item">
            <Link to={`/board/${id}`} className="board-title link">
                {title}
            </Link>
            <IoStarOutline className={`board-item-icon ${isFavorite && "icon-favorite"}`} onClick={handleUpdateBoardFavoriteMutation} />
        </div>
    );
}

const NewBoardItem: React.FC = () => {
    const [createBoardMutation] = useCreateBoardMutation();
    const [board, setBoard] = useState<Partial<Board>>(getNewBoardInitialState);
    const [isNewBoardModalOpen, setNewBoardModalOpen] = useState<boolean>(false);

    const handleOpenModal = () => {
        setNewBoardModalOpen(true);
    }

    const handleCloseModal = () => {
        setNewBoardModalOpen(false);
    }

    const handleCreateBoardMutation = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        try {
            await createBoardMutation(board);
            setBoard(getNewBoardInitialState);
            handleCloseModal();
        }
        catch (e) {
            console.error(e);
        }
    }

    return (
        <>
            <div className="board-item" onClick={handleOpenModal} >
                <h3 className="board-title">Create new board</h3>
                <IoInformationCircleOutline className="board-item-icon icon-info" />
            </div>
            <Modal isVisible={isNewBoardModalOpen} onClose={handleCloseModal}>
                <div className="modal-header">
                    <h2>Add new board</h2>
                    <IoClose className="close" onClick={handleCloseModal} />
                </div>
                <div className="modal-body">
                    <form className="form" onSubmit={e => handleCreateBoardMutation(e)}>
                        <div className="form-group">
                            <label className="form-label" htmlFor="title">Board title</label>
                            <input
                                id="title"
                                type="text"
                                placeholder="Title"
                                className="form-input"
                                value={board.title}
                                onChange={e => setBoard({ ...board, title: e.target.value })}
                            />
                        </div>
                        <div className="form-group">
                            <label className="form-label" htmlFor="private">Private</label>
                            <input
                                id="private"
                                type="checkbox"
                                className="form-checkbox"
                                checked={board.isPrivate}
                                onChange={e => setBoard({ ...board, isPrivate: e.target.checked })}
                            />
                        </div>
                        <button className="submit-btn" type="submit">Create</button>
                    </form>
                </div>
            </Modal>
        </>
    );
}

const BoardsSection: React.FC<IBoardsSectionProps> = ({ boards, type }) => {
    const filteredBoards = useMemo(() => filterBoards(boards, type), [boards, type]);

    return (
        <div className="boards-section">
            <h2>{type}</h2>
            <div className="boards-group">
                {filteredBoards.map(board =>
                    <SectionItem board={board} key={board.id} />
                )}
                {type === SectionType.OwnBoards && <NewBoardItem />}
            </div>
        </div>
    );
};

export default BoardsSection;