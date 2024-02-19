import { Board, Card, Column, RequestTypes } from "../../types/types";
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

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
                credentials: "include"
            }),
            providesTags: ["Board"],
        }),
        getBoardById: builder.query<Board, string>({
            query: (id) => ({
                url: "/Board/GetById",
                method: RequestTypes.GET,
                params: {
                    id
                },
                credentials: "include"
            }),
            providesTags: ["Column"],
        }),
        createBoard: builder.mutation<Board, Partial<Board>>({
            query: (newBoard) => ({
                url: "/Board/Create",
                method: RequestTypes.POST,
                body: newBoard
            }),
            invalidatesTags: ["Board"],
        }),
        updateBoard: builder.mutation<Board, Partial<Board>>({
            query: (updatedBoard) => ({
                url: "/Board/Update",
                method: RequestTypes.PATCH,
                body: updatedBoard
            }),
            invalidatesTags: ["Board"],
        }),
        deleteBoard: builder.mutation({
            query: (boardId) => ({
                url: "/Board/Delete",
                method: RequestTypes.DELETE,
                body: boardId
            }),
        }),
        createColumn: builder.mutation<Column, Partial<Column>>({
            query: (newColumn) => ({
                url: "/Column/Create",
                method: RequestTypes.POST,
                body: newColumn
            }),
            invalidatesTags: ["Column"],
        }),
        deleteColumn: builder.mutation<void, { id: string, boardId: string }>({
            query: (column) => ({
                url: "/Column/Delete",
                method: RequestTypes.DELETE,
                body: column
            }),
            invalidatesTags: ["Column"],
        }),
        createCard: builder.mutation<Card, Partial<Card>>({
            query: (newCard) => ({
                url: "/Card/Create",
                method: RequestTypes.POST,
                body: newCard
            }),
            invalidatesTags: ["Column"],
        })
    }),
});

export const {
    useGetBoardsQuery,
    useGetUserInfoQuery,
    useGetBoardByIdQuery,
    useCreateCardMutation,
    useCreateBoardMutation,
    useUpdateBoardMutation,
    useCreateColumnMutation,
    useDeleteColumnMutation,
} = apiSlice;