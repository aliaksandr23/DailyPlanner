export type Board = {
    id: string,
    title: string,
    isPrivate: boolean,
    isFavorite: boolean,
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
    endDate: string | null,
    startDate: string | null,
    priority: CardPriority,
    columnId: string,
    column: Column,
}

export enum CardPriority {
    None = "None",
    Low = "Low",
    Medium = "Medium",
    High = "High",
}

export enum SectionType {
    Favorite = "Favorite",
    OwnBoards = "Own boards",
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

export interface IGetBoardByIdCommand {
    id: string,
}

export interface ICreateBoardCommand {
    title: string,
    isPrivate: boolean,
}

export interface IUpdateBoardCommand {
    id: string,
    title: string,
    isPrivate: boolean,
    isFavorite: boolean,
}

export interface IDeleteBoardCommand {
    id: string,
}

export interface ICreateColumnCommand {
    title: string,
    boardId: string,
}

export interface IUpdateColumnCommand {
    id: string,
    title: string,
    boardId: string,
}

export interface IDeleteColumnCommand {
    id: string,
    boardId: string,
}

export interface IGetCardByIdCommand {
    id: string,
    boardId: string,
}

export interface ICreateCardCommand {
    columnId: string,
    title: string,
    description: string,
    endDate: string | null,
    startDate: string | null,
    priority: CardPriority,
}

export interface IUpdateCardCommand {
    id: string,
    boardId: string,
    title: string,
    description: string,
    endDate: string | null,
    startDate: string | null,
    priority: CardPriority,
}

export interface IDeleteCardCommand {
    id: string,
    boardId: string,
}