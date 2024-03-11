import { format } from "date-fns";
import { IoClose } from "react-icons/io5";
import { ChangeEvent, useState } from "react";
import { CardPriority } from "../types/types";
import { Modal } from "../components/UI/Modal/Modal";
import { useParams, useNavigate } from "react-router-dom";
import { Spinner } from "../components/UI/Spinner/Spinner";
import { useDeleteCardMutation, useGetCardByIdQuery, useUpdateCardMutation } from "../redux/slices/apiSlice";

const CardDetailsModalPage: React.FC = () => {
    const navigate = useNavigate();
    const [updateCardMutation] = useUpdateCardMutation();
    const [deleteCardMutation] = useDeleteCardMutation();
    const [isEditingPriority, setEditingPriority] = useState<boolean>(false);
    const { cardId, boardId } = useParams<{ boardId: string, cardId: string }>();
    const [updatedPriority, setUpdatedPriority] = useState<CardPriority>(CardPriority.None);
    const { data: card, isLoading, isSuccess } = useGetCardByIdQuery({ id: cardId, boardId });

    const handleCloseCardDetailsModalPage = () => {
        navigate(`/board/${boardId}`);
    }

    if (isLoading) {
        return (
            <Modal onClose={handleCloseCardDetailsModalPage} isVisible={true}>
                <Spinner />
            </Modal>
        );
    }
    else if (isSuccess && card) {
        const { id, title, description, priority, endDate, column } = card;

        const handleUpdateCardTitleMutation = async (e: ChangeEvent<HTMLHeadingElement>) => {
            const newCardTitle = e.target.innerText
            if (newCardTitle !== title) {
                try {
                    await updateCardMutation({ ...card, boardId: column.boardId, title: newCardTitle });
                }
                catch (e) {
                    console.error(e);
                }
            }
        }

        const handleUpdateCardPriorityMutation = async () => {
            if (updatedPriority !== priority) {
                try {
                    await updateCardMutation({ ...card, boardId: column.boardId, priority: updatedPriority })
                }
                catch (e) {
                    console.error(e);
                }
            }
            setEditingPriority(false);
        };

        const handleDeleteCardMutation = async () => {
            try {
                await deleteCardMutation({ id, boardId: column.boardId });
            }
            catch (e) {
                console.error(e);
            }
        }

        const handlePriorityChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
            setUpdatedPriority(e.target.value as CardPriority);
        };

        const handleOpenEditPriority = () => {
            setEditingPriority(true);
        };

        return (
            <Modal onClose={handleCloseCardDetailsModalPage} isVisible={true}>
                <div className="modal-header">
                    <h2
                        className="card-title"
                        contentEditable={true}
                        onBlur={handleUpdateCardTitleMutation}
                        suppressContentEditableWarning={true}
                    >
                        {title}
                    </h2>
                    <IoClose className="close" onClick={handleCloseCardDetailsModalPage} />
                </div>
                <div className="modal-body">
                    <div className="view">
                        <div className="view-group">
                            <p className="view-label">In column:</p>
                            <p className="view-data">{column.title}</p>
                        </div>
                        {endDate && (
                            <div className="view-group">
                                <p className="view-label">Due date:</p>
                                <p className="view-data">{format(new Date(endDate), "EEE MMM dd yyyy HH:mm")}</p>
                            </div>
                        )}
                        <div className="view-group">
                            <p className="view-label">Priority:</p>
                            {isEditingPriority ? (
                                <select
                                    id="priority"
                                    className="view-select"
                                    value={updatedPriority}
                                    onChange={handlePriorityChange}
                                    onBlur={handleUpdateCardPriorityMutation}>
                                    {Object.values(CardPriority).map((val) => (
                                        <option key={val} value={val}>
                                            {val}
                                        </option>
                                    ))}
                                </select>
                            ) : (
                                <p className="view-data" onClick={handleOpenEditPriority}>{priority}</p>
                            )}
                        </div>
                        <div className="view-group description-group">
                            <p className="view-label">Description:</p>
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
    return (<Modal onClose={handleCloseCardDetailsModalPage} isVisible={true}>Error</Modal>);
}

export default CardDetailsModalPage;