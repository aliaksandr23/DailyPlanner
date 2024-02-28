import { ChangeEvent, useState } from "react";
import CardItem from "./CardItem";
import { Modal } from "./UI/Modal/Modal";
import { IoClose } from "react-icons/io5";
import { Card, CardPriority, Column, ICardViewData } from "../types/types";
import { useDeleteColumnMutation, useCreateCardMutation, useUpdateColumnMutation } from "../redux/slices/apiSlice";

interface IColumnItemProps {
    column: Column,
    cardDetailsViewModalHandler: (cardData: ICardViewData) => void,
}

const getNewCardInitialState = (): Partial<Card> => ({
    title: "",
    description: "",
    startDate: null,
    endDate: null,
    priority: CardPriority.None
});

const ColumnItem: React.FC<IColumnItemProps> = ({ column, cardDetailsViewModalHandler }) => {
    const { id, boardId, title, cards } = column;
    const [createCardMutation] = useCreateCardMutation();
    const [updateColumnMutation] = useUpdateColumnMutation();
    const [deleteColumnMutation] = useDeleteColumnMutation();
    const [isNewCardModalOpen, setNewCardModalOpen] = useState<boolean>(false);
    const [newCard, setNewCard] = useState<Partial<Card>>({...getNewCardInitialState, columnId: column.id});

    const handleOpenModal = () => {
        setNewCardModalOpen(true);
    }

    const handleCloseModal = () => {
        setNewCardModalOpen(false);
    }

    const handleDeleteColumnMutation = async () => {
        try {
            await deleteColumnMutation({ id: column.id, boardId: column.boardId });
        }
        catch (e) {
            console.error(e);
            //add a pop-up window with an error and a request to try again
        }
    }

    const handleUpdateColumnTitleMutation = async (e: ChangeEvent<HTMLHeadingElement>) => {
        const newColumnTitle = e.target.innerText
        if (newColumnTitle !== title) {
            try {
                await updateColumnMutation({ id, boardId, title: newColumnTitle });
            }
            catch (e) {
                console.error(e);
            }
        }
    };

    const handleCreateCardMutation = async (e: React.FormEvent<HTMLFormElement>) => {
        try {
            e.preventDefault();
            await createCardMutation(newCard);
            handleCloseModal();
            setNewCard(getNewCardInitialState);
        }
        catch (e) {
            console.error(e);
            //add a pop-up window with an error and a request to try again
        }
    }

    return (
        <div className="column-item">
            <div className="column-header">
                <h3
                    className="col-title"
                    contentEditable={true}
                    suppressContentEditableWarning={true}
                    onBlur={handleUpdateColumnTitleMutation}
                >{title}</h3>
                <IoClose className="icon-delete" onClick={handleDeleteColumnMutation} />
            </div>
            <div className="card-section">
                {cards?.map((card) =>
                    <CardItem
                        key={card.id}
                        card={{ ...card, columnTitle: title }}
                        cardDetailsViewModalHandler={cardDetailsViewModalHandler}
                    />
                )}
            </div>
            <div className="column-footer">
                <button className="new-card-btn" onClick={handleOpenModal}>Add new card</button>
                <Modal isVisible={isNewCardModalOpen} onClose={handleCloseModal}>
                    <div className="modal-header">
                        <h2>Add new card</h2>
                        <IoClose className="close" onClick={handleCloseModal} />
                    </div>
                    <div className="modal-body">
                        <form className="form" onSubmit={e => handleCreateCardMutation(e)}>
                            <div className="form-group">
                                <label className="form-label" htmlFor="card-title">Title</label>
                                <input
                                    id="card-title"
                                    type="text"
                                    placeholder="Title"
                                    className="form-input"
                                    value={newCard.title}
                                    onChange={e => setNewCard({ ...newCard, title: e.target.value })}
                                />
                            </div>
                            <div className="date-time-group">
                                <div className="form-group">
                                    <label className="form-label" htmlFor="start-date">Start date</label>
                                    <input
                                        id="start-date"
                                        type="datetime-local"
                                        className="form-input"
                                        value={newCard.startDate ?? ""}
                                        onChange={e => setNewCard({ ...newCard, startDate: e.target.value })}
                                    />
                                </div>
                                <div className="form-group">
                                    <label className="form-label" htmlFor="end-date">End date</label>
                                    <input
                                        id="end-date"
                                        type="datetime-local"
                                        className="form-input"
                                        value={newCard.endDate ?? ""}
                                        onChange={e => setNewCard({ ...newCard, endDate: e.target.value })}
                                    />
                                </div>
                            </div>
                            <div className="form-group">
                                <label className="form-label" htmlFor="priority">Priority</label>
                                <select
                                    id="priority"
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
                                <label className="form-label" htmlFor="description">Description</label>
                                <textarea
                                    id="description"
                                    rows={3}
                                    placeholder="Description"
                                    className="form-input"
                                    value={newCard.description}
                                    onChange={e => setNewCard({ ...newCard, description: e.target.value })}
                                />
                            </div>
                            <button className="submit-btn" type="submit">Create</button>
                        </form>
                    </div>
                </Modal>
            </div>
        </div>
    );
}

export default ColumnItem;