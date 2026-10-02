import { Container, AppBar, Toolbar, Typography, Button } from "@mui/material";
import { useParams, useNavigate } from "react-router-dom";

// import LineupTimeline from '../components/LineupTimeline';
import type { Lineup } from '../types';

// import CustomForm from '@/components/CustomForm';
// import { type CustomFormField } from '@/components/FormFields';

import { useQuery } from '@tanstack/react-query';
import { lineupQueryOptions } from '../api/queryOptions';

// import { useLineupItemCreateHook } from '../hooks/lineupItemHooks';
import { useLineupUpdateHook} from '../hooks/lineupHooks'


import JsonEditor from '../components/Editor';

interface LineupDataWrapperProps {
    lineup: Lineup;
}

const LineupDataWrapper = ({ lineup }: LineupDataWrapperProps) => {

    const { mutate: updateLineup } = useLineupUpdateHook();

    const saveLineup = async (data: string) => {
        const newlineup = {
            lineupName: lineup.lineupName,
            lineupId: lineup.lineupId,
            channelId: lineup.channelId,
            startTime: lineup.startTime,
            jsonData: data
        } as Lineup;

        updateLineup(newlineup);
    }

    return (
        <JsonEditor lineupJson={lineup.jsonData} save={saveLineup} />
    );
}

const LineupEdit = () => {
    const { id } = useParams();

    const { data: lineup } = useQuery(lineupQueryOptions(Number(id ?? '0')));

    const navigate = useNavigate();


    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Editing Lineup: {id}
                    </Typography>
                    <Button onClick={() => navigate(-1)}> Back</Button>
                </Toolbar>
            </AppBar>   
            {lineup && <LineupDataWrapper lineup={lineup} />}
        </Container>
    )
}


export default LineupEdit;