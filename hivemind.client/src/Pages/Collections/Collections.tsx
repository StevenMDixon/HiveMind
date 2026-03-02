
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import CardActions from '@mui/material/CardActions';
import Container from '@mui/material/Container';
import Button from '@mui/material/Button';
import Toolbar from '@mui/material/Toolbar';
import AppBar from '@mui/material/AppBar';

import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';
import TextField from '@mui/material/TextField';

import RoundedLoadingFiller from '../Components/RoundedLoadingFiller';

import { useEffect, useState } from 'react';

import { useGlobalNotification } from '../../Dashboard/useGlobalNotification';

import { Link } from "react-router-dom";

interface Collection {
    collectionID: number;
    collectionName: string;
}

const CollectionPage = () => {

    const [createModalState, setCreateModalState] = useState<boolean>(false);
    const handleModalOpen = () => setCreateModalState(true);
    const handleModalClose = () => setCreateModalState(false);

    const { showNotification } = useGlobalNotification();

    const [collections, setCollections] = useState<Collection[]>([]);

    const [collectionName, setCollectionName] = useState<string>('');

    const createCollection = async () => {
        const response = await fetch('/collections', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ CollectionName: collectionName }),
        });

        if (response.ok) {
            setCollectionName('');
            showNotification("Cpllection Created", "success");
        } else {
            showNotification("Failed to create collection", "error");
        }

        handleModalClose();
        fetchCollections();
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

        fetchCollections();
    };

    const fetchCollections = async () => {
        const response = await fetch('/collections');

        if (response.ok) {
            const data = await response.json();
            setCollections(data.collections);
        }
    };

    useEffect(() => {
        fetchCollections();
    }, []);

    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Collections
                    </Typography>
                    <Button onClick={handleModalOpen}>Add Collection</Button>
                </Toolbar>
            </AppBar>
            {collections.length > 0 ? collections.map(collection => (
                <Card key={collection.collectionID} sx={{ m: 2 }}>
                    <CardContent>
                        <Typography gutterBottom sx={{ color: 'text.primary', fontSize: 14 }}>
                            {collection.collectionID}
                        </Typography>
                        <Typography gutterBottom sx={{ color: 'text.primary', fontSize: 14 }}>
                            {collection.collectionName}
                        </Typography>
                    </CardContent>
                    <CardActions sx={{ justifyContent: 'flex-end' }}>
                        <Button size="small" color="primary" onClick={() => deleteCollection(collection.collectionID)}>
                            Delete
                        </Button>
                        <Button size="small" color="primary">
                            <Link to={`/Collection/${collection.collectionID}`}>Edit</Link>
                        </Button>
                    </CardActions>
                </Card>
            )) : <RoundedLoadingFiller size={7} />
            }
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