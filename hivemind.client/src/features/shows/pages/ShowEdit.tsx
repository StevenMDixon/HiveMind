import { Container, AppBar, Toolbar, Typography, Button } from "@mui/material";

import { useNavigate, useParams } from "react-router-dom";

import ShowEditLayout from '../components/ShowEditLayout';
import { showQueryOptions, ratingOptionsQueryOptions } from '../api/queryOptions';

import { useQuery } from '@tanstack/react-query';

const ShowEdit = () => {
    const { id } = useParams();

    const navigate = useNavigate();
    

    const { data: ratingOptions } = useQuery(ratingOptionsQueryOptions());
    const { data: show } = useQuery(showQueryOptions(id ?? '0'));


    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Editing Show: {id}
                    </Typography>
                    <Button onClick={() => navigate(-1)}> Back</Button>
                </Toolbar>
            </AppBar>
            {show && ratingOptions && <ShowEditLayout show={show} ratings={ratingOptions} />}
        </Container>
    )
}

export default ShowEdit;