import { Container, AppBar, Toolbar, Typography, Button } from "@mui/material";

import { useNavigate, useParams } from "react-router-dom";

import DroneEditLayout from '../components/DroneEditLayout';

import { droneQueryOptions, stationsQueryOptions } from '../api/queryOptions';
import { useQuery } from '@tanstack/react-query';

const LibraryEdit = () => {
    const { id } = useParams();

    const { data: drone } = useQuery(droneQueryOptions(id ?? '0'));
    const { data: stations } = useQuery(stationsQueryOptions());

    const navigate = useNavigate();

    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Editing Drone: {id}
                    </Typography>
                    <Button onClick={() => navigate(-1)}> Back</Button>
                </Toolbar>
            </AppBar>
            {drone && stations && <DroneEditLayout drone={drone} stations={stations} />}
        </Container>
    )
}

export default LibraryEdit;