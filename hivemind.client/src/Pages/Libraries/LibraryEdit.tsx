import { Container, AppBar, Toolbar, Typography, Button } from "@mui/material";
import { Suspense, useState } from "react";

import ErrorBoundary from "../../Components/ErrorBoundary";
import { useNavigate, useParams } from "react-router-dom";

import LibraryEditLayout from './LibraryEditLayout';
import type { Library } from './types';

const fetchLibrary = async (id: string | undefined): Promise<Library> => {
    const response = await fetch('/libraries/' + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch library: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
};

const fetchLibraryTypes = async () => {
    const response = await fetch('/libraries/types');
    if (response.ok) {
        const data = await response.json();
        return data.types
    }
    return [];
};


const LibraryEdit = () => {
    const { id } = useParams();

    const [libraryPromise] = useState(() => fetchLibrary(id));
    const [libraryTypesPromise] = useState(() => fetchLibraryTypes());

    const navigate = useNavigate();

    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Editing Library: {id}
                    </Typography>
                    <Button onClick={() => navigate(-1)}> Back</Button>
                </Toolbar>
            </AppBar>
            <ErrorBoundary
                fallback={(error) => (
                    <p>{error.message}</p>
                )}>
                <Suspense fallback={<p> Loading </p>}>
                    <LibraryEditLayout libraryPromise={libraryPromise} libraryTypesPromise={libraryTypesPromise} />
                </Suspense>
            </ErrorBoundary>
        </Container>
    )
}

export default LibraryEdit;