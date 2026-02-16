
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

import RoundedLoadingFiller from './Components/RoundedLoadingFiller';

import { useEffect, useState } from 'react';

import { useGlobalNotification } from '../Dashboard/useGlobalNotification';

interface Library {
    libraryId: number;
    libraryName: string;
    libraryPath: string;
}

const LibraryPage = () => {

    const [createModalState, setCreateModalState] = useState<boolean>(false);
    const handleModalOpen = () => setCreateModalState(true);
    const handleModalClose = () => setCreateModalState(false);

    const { showNotification } = useGlobalNotification();

    const [libraries, setLibraries] = useState<Library[]>([]);

    const [libraryName, setLibraryName] = useState<string>('');

    const createLibrary = async () => {
        const response = await fetch('/libraries', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ LibraryName: libraryName, LibraryPath: "" }),
        });

        if (response.ok) {
            setLibraryName('');
            showNotification("Library Created", "success");
        } else {
            showNotification("Failed to create library", "error");
        }

        handleModalClose();
        fetchLibraries();
    };

    const deleteLibrary = async (libraryId: number) => {
        console.log(libraryId)
        const response = await fetch(`/libraries/${libraryId}`, {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' }
        });

        if (response.ok) {
            showNotification("Library deleted", "success")
        } else {
            showNotification("Failed to delete library", "error");
        }

        fetchLibraries();
    };

    const fetchLibraries = async () => {
        const response = await fetch('/libraries'); 
        if (response.ok) {
            const data = await response.json();
            setLibraries(data.libraries);
        }
    };

    useEffect(() => {
        fetchLibraries();
    }, []);

    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Libraries
                    </Typography>
                    <Button onClick={handleModalOpen}>Add Library</Button>
                </Toolbar>
            </AppBar>
            {libraries.length > 0 ? libraries.map(library => (
                <Card key={library.libraryId} sx={{ m: 2 }}>
                    <CardContent>
                        <Typography gutterBottom sx={{ color: 'text.primary', fontSize: 14 }}>
                            {library.libraryId}
                        </Typography>
                        <Typography gutterBottom sx={{ color: 'text.secondary', fontSize: 14 }}>
                            {library.libraryName}
                        </Typography>
                    </CardContent>
                    <CardActions sx={{ justifyContent: 'flex-end' }}>
                        <Button size="small" color="primary" onClick={() => deleteLibrary(library.libraryId)}>
                            Delete
                        </Button>
                        <Button size="small" color="primary">
                            Edit
                        </Button>
                    </CardActions>
                </Card>
            )) : <RoundedLoadingFiller size={7} />
            }
            <Dialog onClose={handleModalClose} open={createModalState}>
                <DialogTitle>Create Library</DialogTitle>
                <DialogContent>
                    <TextField
                        required
                        id="outlined-required"
                        label="Library Name"
                        value={libraryName}
                        onChange={(event: React.ChangeEvent<HTMLInputElement>) => setLibraryName(event.target.value)}
                        sx={{ m: 1 }}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={createLibrary}>Create</Button>
                </DialogActions>
            </Dialog>
        </Container>
    )
}

export default LibraryPage;