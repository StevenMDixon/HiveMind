import { Container, AppBar, Toolbar, Typography, Button, Box } from "@mui/material";
import CircularProgress from '@mui/material/CircularProgress';

import { useNavigate, useParams } from "react-router-dom";

import TransitionEditLayout from '../components/TransitionEditLayout';

import { transitionQueryOptions } from '../api/queryOptions';

import { useQuery } from '@tanstack/react-query';

const TransitionEdit = () => {
    const { id } = useParams();

    const { data } = useQuery(transitionQueryOptions(id ?? '0'));

    const navigate = useNavigate();

    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Editing Transition: {id}
                    </Typography>
                    <Button onClick={() => navigate(-1)}> Back</Button>
                </Toolbar>
            </AppBar>  
            {!data ?
                <Box sx={{ display: 'flex' }}>
                    <CircularProgress aria-label="Loading…" />
                </Box> 
                :
                <TransitionEditLayout transitionData={data} />
            }
        </Container>
    )
}

export default TransitionEdit;