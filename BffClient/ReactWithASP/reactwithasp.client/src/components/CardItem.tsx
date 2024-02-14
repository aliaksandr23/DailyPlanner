import { Card } from "../types/types";

interface CardItemProps {
    card: Card
}

const CardItem: React.FC<CardItemProps> = ({ card }) => {
    const { title } = card;

    return (
        <li className="card-item">
            <h4 className="card-title">{title}</h4>
            <h4 className="card-title">{card.id}</h4>
        </li>
    );
}

export default CardItem;