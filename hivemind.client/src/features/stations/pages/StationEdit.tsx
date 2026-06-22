import { Container, AppBar, Toolbar, Typography, Button, Box } from "@mui/material";
import CircularProgress from '@mui/material/CircularProgress';

import { useNavigate, useParams } from "react-router-dom";

import StationEditLayout from '../components/StationEditLayout';

import { stationQueryOptions } from '../api/queryOptions';

import { useQuery } from '@tanstack/react-query';

const StationEdit = () => {
    const { id } = useParams();

    const { data } = useQuery(stationQueryOptions(id ?? '0'));

    const navigate = useNavigate();

    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Editing Station: {id}
                    </Typography>
                    <Button onClick={() => navigate(-1)}> Back</Button>
                </Toolbar>
            </AppBar>  
            {!data ?
                <Box sx={{ display: 'flex' }}>
                    <CircularProgress aria-label="Loading…" />
                </Box> 
                :
                <StationEditLayout stationData={data} />
            }
        </Container>
    )
}

export default StationEdit;