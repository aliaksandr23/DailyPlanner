import { format, parseISO, isAfter } from "date-fns";
import { ICardViewData } from "../types/types";
import { IoTimeOutline } from "react-icons/io5";

interface ICardItemProps {
    card: ICardViewData,
    openCardDetailsModal: (cardData: ICardViewData) => void,
}

interface ITimeSectionProps {
    startDate: string | null;
    endDate: string | null;
}

const isExpired = (endDate: string | null): boolean => {
    if (!endDate) {
        return false;
    }
    const convertedDate = parseISO(endDate);
    return isAfter(new Date(), convertedDate);
}

const TimeSection: React.FC<ITimeSectionProps> = ({ startDate, endDate }) => {
    if (!endDate && !startDate) {
        return null;
    }

    const formattedStartDate = startDate ? format(new Date(startDate), "dd MMM") : "";
    const formattedEndDate = endDate ? format(new Date(endDate), "dd MMM") : "";

    return (
        <div className="date-section">
            <div className="date"><IoTimeOutline /> {formattedStartDate} {startDate && endDate && '-'} {formattedEndDate}</div>
            {isExpired(endDate) && (<span className="badge-danger">expired</span>)}
        </div>
    );
}

const CardItem: React.FC<ICardItemProps> = ({ card, openCardDetailsModal }) => {
    return (
        <div className="card-item" onClick={() => openCardDetailsModal(card)}>
            <h3 className="card-title">{card.title}</h3>
            <TimeSection startDate={card.startDate} endDate={card.endDate} />
        </div>
    );
}

export default CardItem;