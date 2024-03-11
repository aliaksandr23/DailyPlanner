import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import {
    Board,
    Card,
    Column,
    RequestTypes,
    IGetBoardByIdCommand,
    ICreateBoardCommand,
    IUpdateBoardCommand,
    IDeleteBoardCommand,
    ICreateColumnCommand,
    IDeleteColumnCommand,
    IUpdateColumnCommand,
    ICreateCardCommand,
    IDeleteCardCommand,
    IUpdateCardCommand,
    IGetCardByIdCommand
} from "../../types/types";

export const apiSlice = createApi({
    reducerPath: "dailyPlannerApi",
    baseQuery: fetchBaseQuery({
        baseUrl: "https://localhost:5173",
        headers: {
            "X-CSRF": "1",
        }
    }),
    tagTypes: ["Board", "Column"],
    endpoints: builder => ({
        getUserInfo: builder.query<object, void>({
            query: () => ({
                url: "/account/user",
                method: RequestTypes.GET,
                credentials: "include"
            }),
        }),
        getBoards: builder.query<Board[], void>({
            query: () => ({
                url: "/Board/GetAll",
                method: RequestTypes.GET,
            }),
            providesTags: ["Board"],
        }),
        getBoardById: builder.query<Board, IGetBoardByIdCommand>({
            query: (getBoardByIdCommand) => ({
                url: "/Board/GetById",
                method: RequestTypes.GET,
                params: {
                    id: getBoardByIdCommand.id
                },
            }),
            providesTags: ["Column", "Board"],
        }),
        createBoard: builder.mutation<Board, ICreateBoardCommand>({
            query: (createBoardCommand) => ({
                url: "/Board/Create",
                method: RequestTypes.POST,
                body: createBoardCommand
            }),
            invalidatesTags: ["Board"],
        }),
        updateBoard: builder.mutation<Board, IUpdateBoardCommand>({
            query: (updateBoardCommand) => ({
                url: "/Board/Update",
                method: RequestTypes.PATCH,
                body: updateBoardCommand
            }),
            invalidatesTags: ["Board"],
        }),
        deleteBoard: builder.mutation<void, IDeleteBoardCommand>({
            query: (deleteBoardCommand) => ({
                url: "/Board/Delete",
                method: RequestTypes.DELETE,
                body: deleteBoardCommand
            }),
            invalidatesTags: ["Board"]
        }),
        createColumn: builder.mutation<Column, ICreateColumnCommand>({
            query: (createColumnCommand) => ({
                url: "/Column/Create",
                method: RequestTypes.POST,
                body: createColumnCommand
            }),
            invalidatesTags: ["Column"],
        }),
        deleteColumn: builder.mutation<void, IDeleteColumnCommand>({
            query: (deleteColumnCommand) => ({
                url: "/Column/Delete",
                method: RequestTypes.DELETE,
                body: deleteColumnCommand
            }),
            invalidatesTags: ["Column"],
        }),
        updateColumn: builder.mutation<void, IUpdateColumnCommand>({
            query: (updateColumnCommand) => ({
                url: "/Column/Update",
                method: RequestTypes.PATCH,
                body: updateColumnCommand,
            }),
            invalidatesTags: ["Board"],
        }),
        createCard: builder.mutation<Card, ICreateCardCommand>({
            query: (createCardCommand) => ({
                url: "/Card/Create",
                method: RequestTypes.POST,
                body: createCardCommand
            }),
            invalidatesTags: ["Column"],
        }),
        deleteCard: builder.mutation<void, IDeleteCardCommand>({
            query: (deleteCardCommand) => ({
                url: "/Card/Delete",
                method: RequestTypes.DELETE,
                body: deleteCardCommand
            }),
            invalidatesTags: ["Column"],
        }),
        updateCard: builder.mutation<void, IUpdateCardCommand>({
            query: (updateCardCommand) => ({
                url: "/Card/Update",
                method: RequestTypes.PATCH,
                body: updateCardCommand,
            }),
            invalidatesTags: ["Board", "Column"],
        }),
        getCardById: builder.query<Card, IGetCardByIdCommand>({
            query: (getCardByIdCommand) => ({
                url: "/Card/GetById",
                method: RequestTypes.GET,
                params: {
                    id: getCardByIdCommand.id,
                    boardId: getCardByIdCommand.boardId
                },
            }),
            providesTags: ["Column", "Board"],
        }),
    }),
});

export const {
    useGetUserInfoQuery,
    useGetBoardsQuery,
    useGetBoardByIdQuery,
    useDeleteBoardMutation,
    useCreateBoardMutation,
    useUpdateBoardMutation,
    useCreateColumnMutation,
    useDeleteColumnMutation,
    useUpdateColumnMutation,
    useGetCardByIdQuery,
    useCreateCardMutation,
    useDeleteCardMutation,
    useUpdateCardMutation,
} = apiSlice;