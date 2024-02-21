import { useState } from "react";
import CardItem from "./CardItem";
import { Modal } from "./UI/Modal/Modal";
import { IoClose } from "react-icons/io5";
import { Card, CardPriority, Column, ICardViewData } from "../types/types";
import { useDeleteColumnMutation, useCreateCardMutation } from "../redux/slices/apiSlice";

interface ColumnItemProps {
    column: Column,
    openCardDetailsModal: (cardData: ICardViewData) => void,
}

const ColumnItem: React.FC<ColumnItemProps> = ({ column, openCardDetailsModal }) => {
    const [createCard] = useCreateCardMutation();
    const [deleteColumn] = useDeleteColumnMutation();
    const [isNewCardModalOpen, setNewCardModalOpen] = useState<boolean>(false);
    const getInitialCardState = (columnId: string): Partial<Card> => ({
        columnId,
        title: "",
        description: "",
        startDate: new Date(),
        endDate: new Date(),
        priority: CardPriority.None
    });
    const [newCard, setNewCard] = useState<Partial<Card>>(getInitialCardState(column.id));

    const handleOpenModal = () => {
        setNewCardModalOpen(true);
    }
    const handleCloseModal = () => {
        setNewCardModalOpen(false);
    }
    const resetCardState = () => {
        setNewCard(getInitialCardState(column.id));
    }

    const onDeleteColumnClicked = async () => {
        try {
            await deleteColumn({ id: column.id, boardId: column.boardId });
        }
        catch (e) {
            console.error(e);
            //add a pop-up window with an error and a request to try again
        }
    }

    const onCreateCardClicked = async (e: React.FormEvent<HTMLFormElement>) => {
        try {
            e.preventDefault();
            await createCard(newCard);
            resetCardState();
            handleCloseModal();
        }
        catch (e) {
            console.error(e);
            //add a pop-up window with an error and a request to try again
        }
    }

    return (
        <div className="column-item">
            <div className="column-header">
                <h3>{column.title}</h3>
                <IoClose className="icon-delete" onClick={onDeleteColumnClicked} />
            </div>
            <div className="card-section">
                {column.cards?.map((card) => <CardItem card={{ ...card, columnTitle: column.title }} key={card.id} openCardDetailsModal={openCardDetailsModal} />)}
            </div>
            <div className="column-footer">
                <button className="new-card-btn" onClick={handleOpenModal}>Add new card</button>
                <Modal title="New card" visible={isNewCardModalOpen} onClose={handleCloseModal}>
                    <form className="form" onSubmit={e => onCreateCardClicked(e)}>
                        <div className="form-group">
                            <label className="form-label">Title</label>
                            <input
                                type="text"
                                placeholder="Title"
                                className="form-input"
                                value={newCard.title}
                                onChange={e => setNewCard({ ...newCard, title: e.target.value })}
                            />
                        </div>
                        <div className="date-time-group">
                            <div className="form-group">
                                <label className="form-label">Start date</label>
                                <input
                                    type="datetime-local"
                                    className="form-input"
                                    value={newCard.startDate?.toISOString().slice(0, -8) || ""}
                                    onChange={e => setNewCard({ ...newCard, startDate: new Date(e.target.value) })}
                                />
                            </div>
                            <div className="form-group">
                                <label className="form-label">End date</label>
                                <input
                                    type="datetime-local"
                                    className="form-input"
                                    value={newCard.endDate?.toISOString().slice(0, -8) || ""}
                                    onChange={e => setNewCard({ ...newCard, endDate: new Date(e.target.value) })}
                                />
                            </div>
                        </div>
                        <div className="form-group">
                            <label className="form-label">Priority</label>
                            <select
                                className="form-select"
                                onChange={e => setNewCard({ ...newCard, priority: e.target.value as CardPriority })}
                                value={newCard.priority}>
                                {Object.values(CardPriority).map((val) => (
                                    <option key={val} value={val}>
                                        {val}
                                    </option>
                                ))}
                            </select>
                        </div>
                        <div className="form-group">
                            <label className="form-label">Description</label>
                            <textarea
                                rows={3}
                                placeholder="Description"
                                className="form-input"
                                value={newCard.description}
                                onChange={e => setNewCard({ ...newCard, description: e.target.value })}
                            />
                        </div>
                        <button className="submit-btn" type="submit">Create</button>
                    </form>
                </Modal>
            </div>
        </div>
    );
}

export default ColumnItem;