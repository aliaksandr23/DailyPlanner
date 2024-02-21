import { ICardViewData } from "../types/types";

interface ICardItemProps {
    card: ICardViewData,
    openCardDetailsModal: (cardData: ICardViewData) => void,
}

const CardItem: React.FC<ICardItemProps> = ({ card, openCardDetailsModal }) => {
    return (
        <div className="card-item" onClick={() => openCardDetailsModal(card)}>
            <h4 className="card-title">{card.title}</h4>
            <h4 className="card-title">{card.id}</h4>
        </div>
    );
}

export default CardItem;