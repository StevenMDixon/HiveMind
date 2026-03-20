import Container from '@mui/material/Container';
import Button from '@mui/material/Button';

import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';
import TextField from '@mui/material/TextField';


import { useState } from 'react';

import { useGlobalNotification } from '../../Dashboard/useGlobalNotification';

import CustomTable, { type CellData } from '../Components/Table';

import Header from '../Components/Header';

import { useNavigate } from "react-router-dom";

interface Collection {
    collectionID: number;
    collectionName: string;
}

const fetchCollections = async () => {
    const response = await fetch('/collections');

    if (response.ok) {
        const data = await response.json();
        return data.collections;
    }
};

const CollectionPage = () => {

    const [createModalState, setCreateModalState] = useState<boolean>(false);
    const handleModalOpen = () => setCreateModalState(true);
    const handleModalClose = () => setCreateModalState(false);

    const { showNotification } = useGlobalNotification();

    const [collectionPromise, setCollectionPromise] = useState(() => fetchCollections());

    const [collectionName, setCollectionName] = useState<string>('');

    const navigate = useNavigate();

    const createCollection = async () => {
        const response = await fetch('/collections', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ CollectionName: collectionName }),
        });

        if (response.ok) {
            showNotification("Collection Created", "success");
            const data = await response.json();
            navigate("/collection/" + data.collectionId)
        } else {
            showNotification("Failed to create collection", "error");
        }
    };

    const deleteCollection = async (collectionId: number) => {
        const response = await fetch(`/collections/${collectionId}`, {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' }
        });

        if (response.ok) {
            showNotification("Collection deleted", "success")
        } else {
            showNotification("Failed to delete collection", "error");
        }

        setCollectionPromise(fetchCollections());
    };


    const columns = [
        { key: 'collectionID', name: 'ID', align: 'left' },
        { key: 'collectionName', name: 'Collection Name', align: 'left' }
    ] as CellData<Collection>[];

    const actionColumns = [
        { key: 'a1', name: "Edit", action: (e: Collection) => navigate("/collection/" + e.collectionID), icon: "Edit" },
        { key: 'a2', name: "Delete", action: (e: Collection) => deleteCollection(e.collectionID), icon: "Delete" }
    ] as CellData<Collection>[];

    const handleRetry = () => {
        setCollectionPromise(fetchCollections());
    };


    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Collections">
                <Button onClick={handleModalOpen}>Add Collection</Button>
            </Header>
            <CustomTable dataPromise={collectionPromise} columns={columns} actionColumns={actionColumns} handleRetry={handleRetry} />
           
            <Dialog onClose={handleModalClose} open={createModalState}>
                <DialogTitle>Create Collection</DialogTitle>
                <DialogContent>
                    <TextField
                        required
                        id="outlined-required"
                        label="Collection Name"
                        value={collectionName}
                        onChange={(event: React.ChangeEvent<HTMLInputElement>) => setCollectionName(event.target.value)}
                        sx={{ m: 1 }}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={createCollection}>Create</Button>
                </DialogActions>
            </Dialog>
        </Container>
    )
}

export default CollectionPage;