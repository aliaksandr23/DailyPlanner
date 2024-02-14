import "./index.scss";
import App from "./App";
import { StrictMode } from "react";
import { store } from "./redux/store";
import { Provider } from "react-redux";
import { createRoot } from "react-dom/client";

createRoot(document.getElementById("root")!).render(
    <StrictMode>
        <Provider store={store}>
            <App />
        </Provider>
    </StrictMode>,
);