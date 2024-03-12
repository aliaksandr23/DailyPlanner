import { format } from "date-fns";
import { IoClose } from "react-icons/io5";
import { Card, CardPriority } from "../types/types";
import { Modal } from "../components/UI/Modal/Modal";
import { ChangeEvent, useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Spinner } from "../components/UI/Spinner/Spinner";
import { useDeleteCardMutation, useGetCardByIdQuery, useUpdateCardMutation } from "../redux/slices/apiSlice";

const CardDetailsModalPage: React.FC = () => {
    const navigate = useNavigate();
    const [updateCardMutation] = useUpdateCardMutation();
    const [deleteCardMutation] = useDeleteCardMutation();
    const [isEditingPriority, setEditingPriority] = useState<boolean>(false);
    const { cardId, boardId } = useParams<{ boardId: string, cardId: string }>();
    const { data: card, isLoading, isSuccess } = useGetCardByIdQuery({ id: cardId, boardId });
    const [viewCardData, setViewCardData] = useState<Partial<Card>>({
        title: "",
        description: "",
        isDone: false,
        startDate: "",
        endDate: "",
        priority: CardPriority.None,
    })

    useEffect(() => {
        if (isSuccess && card) {
            setViewCardData(card);
        }
    }, [isSuccess, card]);

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
        const { title, description, priority, endDate } = viewCardData;

        const handleUpdateCardTitleMutation = async (e: ChangeEvent<HTMLHeadingElement>) => {
            setViewCardData({ ...viewCardData, title: e.target.innerText })
            if (title !== card.title && title) {
                try {
                    await updateCardMutation({ ...card, boardId: card.column.boardId, title });
                }
                catch (e) {
                    console.error(e);
                }
            }
        }

        const handleUpdateCardPriorityMutation = async () => {
            if (priority !== card.priority && priority) {
                try {
                    await updateCardMutation({ ...card, boardId: card.column.boardId, priority })
                }
                catch (e) {
                    console.error(e);
                }
            }
            setEditingPriority(false);
        };

        const handleUpdateCardDescriptionMutation = async () => {
            if (description != card.description && description) {
                try {
                    await updateCardMutation({ ...card, boardId: card.column.boardId, description })
                }
                catch (e) {
                    console.error(e);
                }
            }
        }

        const handleDeleteCardMutation = async () => {
            try {
                await deleteCardMutation({ id: card.id, boardId: card.column.boardId });
                handleCloseCardDetailsModalPage();
            }
            catch (e) {
                console.error(e);
            }
        }

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
                            <p className="view-data">{card.column.title}</p>
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
                                    value={priority}
                                    onChange={(e) => setViewCardData({ ...viewCardData, priority: e.target.value as CardPriority })}
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
                                value={description}
                                onBlur={handleUpdateCardDescriptionMutation}
                                onChange={(e) => setViewCardData({ ...viewCardData, description: e.target.value })}
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