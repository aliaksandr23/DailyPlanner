import { useState } from "react";
import CardItem from "./CardItem";
import { Column } from "../types/types";
import { Modal } from "./UI/Modal/Modal";
import { IoClose } from "react-icons/io5";
import { useDeleteColumnMutation } from "../redux/slices/apiSlice";

interface ColumnItemProps {
    column: Column,
}

const ColumnItem: React.FC<ColumnItemProps> = ({ column }) => {
    const [deleteBoardColumn] = useDeleteColumnMutation();
    const [isModalOpen, setIsModalOpen] = useState<boolean>(false);

    const handleOpenModal = () => {
        setIsModalOpen(true);
    }
    const handleCloseModal = () => {
        setIsModalOpen(false);
    }

    const onSubmitClicked = async () => {
        try {
            await deleteBoardColumn({id: column.id, boardId: column.boardId});
        }
        catch (e) {
            console.error(e);
            //add a pop-up window with an error and a request to try again
        }
    }

    return (
        <div className="column-item">
            <div className="column-header">
                <h3>{column.title}</h3>
                <IoClose className="icon-delete" onClick={onSubmitClicked} />
            </div>
            <ul className="card-list">
                {column.cards?.map((card) => <CardItem card={card} key={card.id} />)}
            </ul>
            <div className="column-footer">
                <button className="new-card-btn" onClick={handleOpenModal}>Add new card</button>
                <Modal title="New card" visible={isModalOpen} onClose={handleCloseModal}>
                </Modal>
            </div>
        </div>
    );
}

export default ColumnItem;