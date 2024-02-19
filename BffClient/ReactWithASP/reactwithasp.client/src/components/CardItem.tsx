import { useState } from "react";
import { Card } from "../types/types";
import { Modal } from "./UI/Modal/Modal";

interface CardItemProps {
    card: Card
}

const CardItem: React.FC<CardItemProps> = ({ card }) => {
    const { title } = card;
    const [isCardDataModalOpen, setCardDataModalOpen] = useState<boolean>(false);
    const handleOpenModal = () => {
        setCardDataModalOpen(true);
    }
    const handleCloseModal = () => {
        setCardDataModalOpen(false);
    }

    return (
        <>
            <li className="card-item" onClick={handleOpenModal}>
                <h4 className="card-title">{title}</h4>
                <h4 className="card-title">{card.id}</h4>
            </li>
            <Modal title={title} onClose={handleCloseModal} visible={isCardDataModalOpen}>
            </Modal>
        </>
    );
}

export default CardItem;