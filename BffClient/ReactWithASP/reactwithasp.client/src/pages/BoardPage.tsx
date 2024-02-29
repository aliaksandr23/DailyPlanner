import { ChangeEvent, useState } from "react";
import { format } from "date-fns";
import ColumnItem from "../components/ColumnItem";
import { Modal } from "../components/UI/Modal/Modal";
import { Spinner } from "../components/UI/Spinner/Spinner";
import { Navigate, useNavigate, useParams } from "react-router-dom";
import { CardPriority, Column, ICardViewData } from "../types/types";
import { IoStarOutline, IoLockClosedOutline, IoLockOpenOutline, IoClose, IoTrashOutline } from "react-icons/io5";
import { useCreateColumnMutation, useDeleteBoardMutation, useDeleteCardMutation, useGetBoardByIdQuery, useUpdateBoardMutation, useUpdateCardMutation } from "../redux/slices/apiSlice";

interface ICardDetailsModalProps {
    isVisible: boolean,
    card: ICardViewData,
    onClose: () => void,
}

interface IBoardDeleteConfirmModal {
    isVisible: boolean,
    boardId: string,
    boardTitle: string,
    onReject: () => void,
}

const initialCardData: Partial<ICardViewData> = {
    title: "",
    description: "",
    endDate: "",
    startDate: "",
    priority: CardPriority.None,
    columnTitle: "",
}

const CardDetailsModal: React.FC<ICardDetailsModalProps> = ({ card, isVisible, onClose }) => {
    const { id, title, columnId, columnTitle, description, priority, endDate } = card
    const [updateCardMutation] = useUpdateCardMutation()
    const [deleteCardMutation] = useDeleteCardMutation();

    const handleUpdateCardTitleMutation = async (e: ChangeEvent<HTMLHeadingElement>) => {
        const newCardTitle = e.target.innerText
        if (newCardTitle !== title) {
            try {
                await updateCardMutation({ id, columnId, title: newCardTitle });
            }
            catch (e) {
                console.error(e);
            }
        }
    }

    const handleDeleteCardMutation = async () => {
        try {
            await deleteCardMutation({ id, columnId });
            onClose();
        }
        catch (e) {
            console.error(e);
            //add a pop-up window with an error and a request to try again
        }
    }

    if (card) {
        return (
            <Modal onClose={onClose} isVisible={isVisible}>
                <div className="modal-header">
                    <h2
                        className="card-title"
                        contentEditable={true}
                        onBlur={handleUpdateCardTitleMutation}
                        suppressContentEditableWarning={true}
                    >
                        {title}
                    </h2>
                    <IoClose className="close" onClick={onClose} />
                </div>
                <div className="modal-body">
                    <div className="view">
                        <div className="view-group">
                            <p className="view-label">In column: <i className="view-data">{columnTitle}</i></p>
                        </div>
                        <div className="view-group">
                            <p className="view-label">Priority: <i className="view-data">{priority}</i></p>
                        </div>
                        <div className="view-group">
                            {endDate && (
                                <p className="view-label">Due date: <i className="view-data">{format(new Date(endDate), "EEE MMM dd yyyy HH:mm")}</i></p>
                            )}
                            <p className="view-label">Description:</p>
                        </div>
                        <div className="view-group">
                            <textarea
                                id="description"
                                rows={3}
                                readOnly
                                value={description}
                                className="view-textarea"
                            />
                        </div>
                        <button className="submit-btn danger" onClick={handleDeleteCardMutation}>Delete</button>
                    </div>
                </div>
            </Modal>
        );
    }
    return (
        <Modal onClose={onClose} isVisible={isVisible}>
            <h2>Something went wrong</h2>
        </Modal>
    );
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
    const { data: board, isLoading, isSuccess } = useGetBoardByIdQuery(boardId);
    const [column, setColumn] = useState<Partial<Column>>({ title: "", boardId: "" });
    const [isNewColumnDropdownOpen, setNewColumnDropdownOpen] = useState<boolean>(false);
    const [isCardDetailsViewModalOpen, setCardDetailsViewModalOpen] = useState<boolean>(false);
    const [isBoardDeleteConfirmModalOpen, setBoardDeleteConfirmModalOpen] = useState<boolean>(false);
    const [selectedCard, setSelectedCardData] = useState<ICardViewData>(initialCardData as ICardViewData);

    if (isLoading) {
        return (<Spinner />);
    }
    else if (isSuccess && board) {
        const { id, title, isFavorite, isPrivate, columns } = board

        const handleCreateColumnMutation = async () => {
            try {
                await createColumnMutation({ ...column, boardId: id });
                setColumn({ title: "" });
                setNewColumnDropdownOpen(false);
            }
            catch (e) {
                console.error(e);
            }
        }

        const handleOpenCardDetailsViewModal = (selectedCard: ICardViewData) => {
            setSelectedCardData(selectedCard);
            setCardDetailsViewModalOpen(true);
        }

        const handleCloseCardDetailsViewModal = () => {
            setCardDetailsViewModalOpen(false);
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
                    await updateBoardMutation({ id, title: newBoardTitle });
                }
                catch (e) {
                    console.error(e);
                }
            }
        };

        const handleUpdateBoardFavoriteMutation = async () => {
            try {
                await updateBoardMutation({ id, isFavorite: !isFavorite });
            }
            catch (e) {
                console.error(e);
            }
        }

        const handleUpdateBoardPrivateMutation = async () => {
            try {
                await updateBoardMutation({ id, isPrivate: !isPrivate });
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
                    {columns?.map((col) => <ColumnItem column={col} key={col.id} cardDetailsViewModalHandler={handleOpenCardDetailsViewModal} />)}
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
                <CardDetailsModal
                    card={selectedCard}
                    isVisible={isCardDetailsViewModalOpen}
                    onClose={handleCloseCardDetailsViewModal}
                />
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