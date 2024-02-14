import { Link } from "react-router-dom";
import { Modal } from "./UI/Modal/Modal";
import { useMemo, useState } from "react";
import { Board, SectionType } from "../types/types";
import { useCreateBoardMutation } from "../redux/slices/apiSlice";
import { IoInformationCircleOutline, IoStarOutline } from "react-icons/io5";

interface BoardsSectionProps {
    boards: Board[],
    type: SectionType,
}

interface SectionItemProps {
    board: Board,
}

const SectionItem: React.FC<SectionItemProps> = ({ board }) => {
    return (
        <div className="board-item">
            <Link to={`/board/${board.id}`} className="board-title link">
                {board.title}
            </Link>
            <IoStarOutline className={`board-item-icon ${board.isFavorite && "icon-favorite"}`} />
        </div>
    );
}

const NewBoardItem: React.FC = () => {
    const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
    const handleOpenModal = () => {
        setIsModalOpen(true);
    }
    const handleCloseModal = () => {
        setIsModalOpen(false);
    }
    const [createNewBoard] = useCreateBoardMutation();
    const [board, setBoard] = useState<Partial<Board>>({
        title: "",
        isPrivate: false,
    });
    const onSubmitClicked = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
            createNewBoard(board);
            setBoard({
                title: "",
                isPrivate: false,
            });
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
            <Modal visible={isModalOpen} title="Add new board" onClose={handleCloseModal}>
                <form className="new-board-modal-form" onSubmit={(e) => onSubmitClicked(e)}>
                    <div className="form-group">
                        <label className="form-label">Board title</label>
                        <input
                            type="text"
                            placeholder="Title"
                            className="form-input"
                            value={board.title}
                            onChange={e => setBoard({ ...board, title: e.target.value })}
                        />
                    </div>
                    <div className="form-group">
                        <label className="form-label">Private</label>
                        <input
                            type="checkbox"
                            className="form-checkbox"
                            checked={board.isPrivate}
                            onChange={e => setBoard({ ...board, isPrivate: e.target.checked })}
                        />
                    </div>
                    <button className="submit-btn" type="submit">Create</button>
                </form>
            </Modal>
        </>
    );
}

const filterBoards = (boards: Board[], type: SectionType): Board[] => {
    switch (type) {
        case SectionType.RecentlyViewed:
            return boards.filter(b => b.lastViewed != null);
        case SectionType.Favorite:
            return boards.filter(b => b.isFavorite);
        default:
            return boards;
    }
};


const BoardsSection: React.FC<BoardsSectionProps> = ({ boards, type }) => {
    const filteredBoards = useMemo(() => filterBoards(boards, type), [boards, type]);

    if (!filteredBoards.length) {
        return null;
    }
    return (
        <div className="boards-section">
            <h2>{type}</h2>
            <div className="boards-group">
                {filteredBoards.map(board => (
                    <SectionItem board={board} key={board.id} />
                ))}
                {type === SectionType.OwnBoards && <NewBoardItem />}
            </div>
        </div>
    );
};

export default BoardsSection;