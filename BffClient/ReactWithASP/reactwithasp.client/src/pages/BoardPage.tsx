import { useState } from "react";
import { format } from "date-fns";
import ColumnItem from "../components/ColumnItem";
import { Modal } from "../components/UI/Modal/Modal";
import { Column, ICardViewData } from "../types/types";
import { Navigate, useParams } from "react-router-dom";
import { Spinner } from "../components/UI/Spinner/Spinner";
import { useCreateColumnMutation, useGetBoardByIdQuery } from "../redux/slices/apiSlice";
import { IoStarOutline, IoLockClosedOutline, IoLockOpenOutline, IoClose } from "react-icons/io5";

interface ICardDetailsModalProps {
    isOpen: boolean,
    card: ICardViewData | null,
    onClose: () => void,
}

const CardDetailsModal: React.FC<ICardDetailsModalProps> = ({ card, isOpen, onClose }) => {
    if (card) {
        return (
            <Modal title={card.title} onClose={onClose} visible={isOpen}>
                <div className="modal-header">
                    <h2>Add new board</h2>
                    <IoClose className="close" onClick={onClose} />
                </div>
                <div className="modal-body">
                    <div className="view-group">
                        <p className="view-label">In column: <i className="view-data">{card.columnTitle}</i></p>
                        <p className="view-label">Priority: <i className="view-data">{card.priority}</i></p>
                        {card.endDate && (
                            <p className="view-label">Due date: <i className="view-data">{format(new Date(card.endDate), "EEE MMM dd yyyy HH:mm")}</i></p>
                        )}
                        <p className="view-label">Description:</p>
                        <textarea
                            id="description"
                            rows={3}
                            readOnly
                            value={card.description}
                            className="view-textarea"
                        />
                    </div>
                </div>
            </Modal>
        );
    }
    return (
        <Modal title="Error" onClose={onClose} visible={isOpen}>
            <h2>Something went wrong</h2>
        </Modal>
    );
}

const BoardPage: React.FC = () => {
    const { boardId } = useParams<{ boardId: string }>();
    const [isNewColumnDropdownOpen, setNewColumnDropdownOpen] = useState<boolean>(false);
    const [isCardDetailsViewModalOpen, setCardDetailsViewModalOpen] = useState<boolean>(false);
    const [column, setColumn] = useState<Partial<Column>>({ title: "", boardId: "" });
    const { data: board, isLoading, isSuccess } = useGetBoardByIdQuery(boardId);
    const [selectedCard, setSelectedCardData] = useState<ICardViewData | null>(null);
    const [createNewColumn] = useCreateColumnMutation();

    const handleSubmit = async () => {
        try {
            await createNewColumn({ ...column, boardId });
            setColumn({ title: "", boardId: "" });
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

    if (isLoading) {
        return (<Spinner />);
    }
    else if (isSuccess && board) {
        return (
            <>
                <div className="board-navbar container">
                    <h2 className="nav-title">
                        {board.title}
                    </h2>
                    <button className="nav-btn">
                        Favorite
                        <IoStarOutline className="icon" />
                    </button>
                    <button className="nav-btn">
                        {board.isPrivate ? "Private" : "Public"}
                        {board.isPrivate ? <IoLockClosedOutline className="icon" /> : <IoLockOpenOutline className="icon" />}
                    </button>
                </div>
                <div className="columns-section">
                    {board.columns?.map((col) => <ColumnItem column={col} key={col.id} openCardDetailsModal={handleOpenCardDetailsViewModal} />)}
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
                                <button className="save-btn" onClick={handleSubmit}>Save</button>
                                <IoClose className="icon-close" onClick={() => setNewColumnDropdownOpen(false)} />
                            </div>
                        )}
                    </div>
                </div>
                <CardDetailsModal card={selectedCard} isOpen={isCardDetailsViewModalOpen} onClose={handleCloseCardDetailsViewModal} />
            </>
        );
    }
    return (<Navigate to="/error" replace />);
}

export default BoardPage;