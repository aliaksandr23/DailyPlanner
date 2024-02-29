import { ICardViewData } from "../types/types";
import { IoTimeOutline } from "react-icons/io5";
import { format, parseISO, isAfter, differenceInHours } from "date-fns";

interface ICardItemProps {
    card: ICardViewData,
    cardDetailsViewModalHandler: (cardData: ICardViewData) => void,
}

interface ITimeSectionProps {
    startDate: string | null,
    endDate: string | null,
}

const TimeBadge: React.FC<{ endDate: string }> = ({ endDate }) => {
    const endDateObj = parseISO(endDate);
    const currentDateObj = new Date();

    if (isAfter(currentDateObj, endDateObj)) {
        return <span className="badge-danger">expired</span>
    }
    else if (differenceInHours(endDateObj, currentDateObj) <= 24) {
        return <span className="badge-warning">due soon</span>
    }

    return null;
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
            {(endDate) && <TimeBadge endDate={endDate} />}
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