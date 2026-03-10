import { useParams, useNavigate } from "react-router-dom";
import { useState, Suspense} from 'react'
import ErrorBoundary from '../../Components/ErrorBoundary';

import Container from '@mui/material/Container';
import Toolbar from '@mui/material/Toolbar';
import AppBar from '@mui/material/AppBar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button'

import { CollectionLayout } from './CollectionLayout'
import type { Collection } from './types'


const fetchCollection = async (collectionId: number): Promise<Collection> => {
    const response = await fetch('/collections/' + collectionId);

    if (!response.ok) {
        throw new Error(`Failed to fetch media: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
};

const CollectionDetail = () => {
    const { id } = useParams();

    const navigate = useNavigate();

    const [collectionPromise, setCollectionPromise] = useState(() => fetchCollection(Number(id)));

    const saveCollection = async (collection: Collection): Promise<void> => {
        const result = await fetch('/collections/'+ id, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(collection),
        });

        if (result.ok) navigate(-1);

        setCollectionPromise(() => fetchCollection(Number(id)))
    }

    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Collection Details
                    </Typography>
                    <Button onClick={()=> navigate(-1) }> Back</Button>
                </Toolbar>
            </AppBar>
            <ErrorBoundary
                fallback={(error) => (
                    <p>{error.message}</p>
                )}>
                <Suspense fallback={<p> Loading </p>}>
                    <CollectionLayout promise={collectionPromise} save={saveCollection} />        
                </Suspense>
            </ErrorBoundary>
        </Container>
    )
}

export default CollectionDetail;