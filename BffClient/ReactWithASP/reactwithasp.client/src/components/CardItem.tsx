import { ICardViewData } from "../types/types";
import { IoTimeOutline } from "react-icons/io5";
import { format, parseISO, isAfter } from "date-fns";

interface ICardItemProps {
    card: ICardViewData,
    cardDetailsViewModalHandler: (cardData: ICardViewData) => void,
}

interface ITimeSectionProps {
    startDate: string | null,
    endDate: string | null,
}

const isExpired = (endDate: string | null): boolean => {
    if (!endDate) {
        return false;
    }
    return isAfter(new Date(), parseISO(endDate));
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

const CardItem: React.FC<ICardItemProps> = ({ card, cardDetailsViewModalHandler }) => {
    const { title, startDate, endDate } = card;

    return (
        <div className="card-item" onClick={() => cardDetailsViewModalHandler(card)}>
            <h3 className="card-title">{title}</h3>
            <TimeSection startDate={startDate} endDate={endDate} />
        </div>
    );
}

export default CardItem;