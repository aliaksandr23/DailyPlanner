export type Board = {
    id: string,
    title: string,
    isPrivate: boolean,
    isFavorite: boolean,
    lastViewed: Date | null;
    columns: Column[] | null,
}

export type Column = {
    id: string,
    title: string,
    cards: Card[] | null,
    boardId: string,
}

export type Card = {
    id: string,
    title: string,
    isDone: boolean,
    description: string,
    endDate: Date,
    startDate: Date,
    priority: CardPriority,
    columnId: string,
}

export interface ICardViewData extends Card {
    columnTitle: string
}

export enum CardPriority {
    None = "None",
    Low = "Low",
    Medium = "Medium",
    High = "High"
}

export enum SectionType {
    Favorite = "Favorite",
    OwnBoards = "Own boards",
    //GuestBoards = "Guest boards",
    //RecentlyViewed = "Recentry Viewed",
}

export enum RequestTypes {
    GET = "GET",
    POST = "POST",
    DELETE = "DELETE",
    PATCH = "PATCH",
    PUT = "PUT",
}

export type Claim = {
    type: string,
    value: string,
}
