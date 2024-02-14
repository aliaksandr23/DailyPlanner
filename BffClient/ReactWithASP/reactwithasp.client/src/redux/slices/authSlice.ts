import { createSlice } from "@reduxjs/toolkit";
import { Claim } from "../../types/types";

interface IAuthContext {
    isAuthorized: boolean,
    claims: Claim[],
    loginUrl: string,
    logoutUrl: string
}

const initialState: IAuthContext = {
    isAuthorized: false,
    claims: [],
    loginUrl: "/account/login",
    logoutUrl: ""
}

const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        setUserData(state, { payload }) {
            state.claims = payload;
            state.isAuthorized = true;
            state.logoutUrl = state.claims.find(claim => claim?.type === "bff:logout_url")?.value ?? "account/logout";
        },
        resetUserData(state) {
            state.claims = [];
            state.isAuthorized = false;
            state.logoutUrl = "";

        }
    },
});

export default authSlice.reducer;
export const { setUserData, resetUserData } = authSlice.actions;