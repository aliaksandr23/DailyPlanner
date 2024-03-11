import { ChangeEvent, useState } from "react";
import ColumnItem from "../components/ColumnItem";
import { Modal } from "../components/UI/Modal/Modal";
import { ICreateColumnCommand } from "../types/types";
import { Spinner } from "../components/UI/Spinner/Spinner";
import { Navigate, Outlet, useNavigate, useParams } from "react-router-dom";
import { IoStarOutline, IoLockClosedOutline, IoLockOpenOutline, IoClose, IoTrashOutline } from "react-icons/io5";
import { useCreateColumnMutation, useDeleteBoardMutation, useGetBoardByIdQuery, useUpdateBoardMutation } from "../redux/slices/apiSlice";

interface IBoardDeleteConfirmModal {
    isVisible: boolean,
    boardId: string,
    boardTitle: string,
    onReject: () => void,
}

const BoardDeleteConfirmModal: React.FC<IBoardDeleteConfirmModal> = ({ isVisible, boardId, boardTitle, onReject }) => {
    const navigate = useNavigate();
    const [deleteBoardMutation] = useDeleteBoardMutation();

    const handleDeteleBoardMutation = async () => {
        try {
            await deleteBoardMutation({ id: boardId });
            navigate("/Boards", { replace: true });
        }
        catch (e) {
            console.error(e);
        }
    }

    return (
        <Modal onClose={onReject} isVisible={isVisible} >
            <div className="modal-header">
                <h2 className="header-title">Delete board: {boardTitle}?</h2>
            </div>
            <div className="modal-body">
                <p className="message">Are you sure want to delete this board?
                    Once a board is deleted, it cannot be restored.</p>
            </div>
            <div className="modal-footer">
                <button className="submit-btn danger" onClick={handleDeteleBoardMutation}>Confirm</button>
                <button className="submit-btn" onClick={onReject}>Reject</button>
            </div>
        </Modal>
    );
}

const BoardPage: React.FC = () => {
    const { boardId } = useParams<{ boardId: string }>();
    const [updateBoardMutation] = useUpdateBoardMutation();
    const [createColumnMutation] = useCreateColumnMutation();
    const { data: board, isLoading, isSuccess } = useGetBoardByIdQuery({ id: boardId });
    const [column, setColumn] = useState<ICreateColumnCommand>({ title: "", boardId: "" });
    const [isNewColumnDropdownOpen, setNewColumnDropdownOpen] = useState<boolean>(false);
    const [isBoardDeleteConfirmModalOpen, setBoardDeleteConfirmModalOpen] = useState<boolean>(false);

    if (isLoading) {
        return (<Spinner />);
    }
    else if (isSuccess && board) {
        const { id, title, isFavorite, isPrivate, columns } = board

        const handleCreateColumnMutation = async () => {
            try {
                await createColumnMutation({ ...column, boardId: id });
                setColumn({ ...column, title: "" });
                setNewColumnDropdownOpen(false);
            }
            catch (e) {
                console.error(e);
            }
        }

        const handleOpenBoardDeleteConfirmModal = () => {
            setBoardDeleteConfirmModalOpen(true);
        }

        const handleCloseBoardDeleteConfirmModal = () => {
            setBoardDeleteConfirmModalOpen(false);
        }

        const handleUpdateBoardTitleMutation = async (e: ChangeEvent<HTMLHeadingElement>) => {
            const newBoardTitle = e.target.innerText
            if (newBoardTitle !== title) {
                try {
                    await updateBoardMutation({ ...board, title: newBoardTitle });
                }
                catch (e) {
                    console.error(e);
                }
            }
        };

        const handleUpdateBoardFavoriteMutation = async () => {
            try {
                await updateBoardMutation({ ...board, isFavorite: !isFavorite });
            }
            catch (e) {
                console.error(e);
            }
        }

        const handleUpdateBoardPrivateMutation = async () => {
            try {
                await updateBoardMutation({ ...board, isPrivate: !isPrivate });
            }
            catch (e) {
                console.error(e);
            }
        }

        return (
            <>
                <div className="board-navbar container">
                    <h2
                        className="board-title"
                        contentEditable={true}
                        onBlur={handleUpdateBoardTitleMutation}
                        suppressContentEditableWarning={true}
                    >
                        {board.title}
                    </h2>
                    <button className="submit-btn" onClick={handleUpdateBoardFavoriteMutation}>
                        Favorite
                        <IoStarOutline className={`icon ${board.isFavorite && "favorite"}`} />
                    </button>
                    <button className="submit-btn" onClick={handleUpdateBoardPrivateMutation} >
                        {board.isPrivate ? "Private" : "Public"}
                        {board.isPrivate ? <IoLockClosedOutline className="icon" /> : <IoLockOpenOutline className="icon" />}
                    </button>
                    <button className="submit-btn danger" onClick={handleOpenBoardDeleteConfirmModal}>
                        Delete board
                        <IoTrashOutline className="icon" />
                    </button>
                </div>
                <div className="columns-section">
                    {columns?.map((col) => <ColumnItem column={col} key={col.id} />)}
                    <div className="new-column-item">
                        <input
                            id="column-title"
                            className="new-col-input"
                            placeholder="Add a new column"
                            value={column.title}
                            onFocus={() => setNewColumnDropdownOpen(true)}
                            onChange={e => setColumn({ ...column, title: e.target.value })}
                        />
                        {isNewColumnDropdownOpen && (
                            <div className="save-section">
                                <button className="save-btn" onClick={handleCreateColumnMutation}>Save</button>
                                <IoClose className="icon-close" onClick={() => setNewColumnDropdownOpen(false)} />
                            </div>
                        )}
                    </div>
                </div>
                <Outlet />
                <BoardDeleteConfirmModal
                    boardId={board.id}
                    boardTitle={board.title}
                    isVisible={isBoardDeleteConfirmModalOpen}
                    onReject={handleCloseBoardDeleteConfirmModal}
                />
            </>
        );
    }
    return (<Navigate to="/error" replace />);
}

export default BoardPage;