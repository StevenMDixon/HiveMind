import { Container, AppBar, Toolbar, Typography, Button } from "@mui/material";

import { useNavigate, useParams } from "react-router-dom";

import LibraryEditLayout from '../components/LibraryEditLayout';

import { libraryQueryOptions, libraryTypesQueryOptions } from '../api/queryOptions';
import { useQuery } from '@tanstack/react-query';

const LibraryEdit = () => {
    const { id } = useParams();

    const { data: library } = useQuery(libraryQueryOptions(id ?? '0'));
    const { data: libraryTypes } = useQuery(libraryTypesQueryOptions());

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
            {library && libraryTypes && <LibraryEditLayout library={library} libraryTypes={libraryTypes} />}
        </Container>
    )
}

export default LibraryEdit;