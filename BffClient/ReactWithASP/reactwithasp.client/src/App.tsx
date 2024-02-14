import { useEffect } from "react";
import AppRouter from "./AppRouter";
import { RouterProvider } from "react-router-dom";
import { setUserData } from "./redux/slices/authSlice";
import { useAppDispatch } from "./redux/hooks/authHooks";
import { Spinner } from "./components/UI/Spinner/Spinner";
import { useGetUserInfoQuery } from "./redux/slices/apiSlice";

const App = () => {
    const dispatch = useAppDispatch();
    const {
        data: userClaims,
        isLoading,
        isSuccess,
    } = useGetUserInfoQuery(undefined, {
        pollingInterval: 300000, //perform a refetch every 5 mins
    });

    useEffect(() => {
        if (isSuccess) {
            dispatch(setUserData(userClaims));
        }
    }, [isSuccess, userClaims, dispatch]);

    if (isLoading) {
        return (<Spinner />);
    }
    else {
        return (<RouterProvider router={AppRouter} />);
    }
}

export default App;