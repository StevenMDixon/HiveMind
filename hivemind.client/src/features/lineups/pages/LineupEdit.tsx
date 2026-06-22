import { Container, AppBar, Toolbar, Typography, Stack, Box, Paper, Button } from "@mui/material";
import { useParams, useNavigate } from "react-router-dom";

import LineupTimeline from '../components/LineupTimeline';
import type { LineupItem, Lineup } from '../types';

import CustomForm from '@/components/CustomForm';
import { type CustomFormField } from '@/components/FormFields';

import { useQuery } from '@tanstack/react-query';
import { lineupQueryOptions } from '../api/queryOptions';

import { useLineupItemCreateHook } from '../hooks/lineupItemHooks';
import { useLineupUpdateHook} from '../hooks/lineupHooks'


interface LineupDataWrapperProps {
    lineup: Lineup;
}

const LineupDataWrapper = ({ lineup }: LineupDataWrapperProps) => {

    const { mutate: createLineupItem } = useLineupItemCreateHook();
    const { mutate: updateLineup } = useLineupUpdateHook();

    const lineupFields = [
        { name: 'lineupName', display: "Name", type: "Text", initialValue: lineup.lineupName },
        { name: 'startTime', display: "Start Time", type: "Text", initialValue: lineup.startTime ?? "" }
    ] as CustomFormField[];

    const saveLineup = async (editedLineup: Lineup) => {
        editedLineup.lineupItems = lineup.lineupItems;
        updateLineup(editedLineup);
    }

    const navigate = useNavigate();

    const setLineupItemToEdit = (e: LineupItem) => {
        navigate("/lineups/" + lineup.lineupId + "/items/" + e.lineupItemId)
    }

    const AddLineupItem = async () => {
        const currentIdx = (lineup.lineupItems.length > 0 ? lineup.lineupItems[lineup.lineupItems.length -1].index : 0) + 1;

        const newItem = {
            lineupId: lineup.lineupId,
            lineupItemId: -currentIdx,
            name: 'New ' + currentIdx,
            index: currentIdx,
            type: 'Generic',
            queries: []
        } as LineupItem;

        createLineupItem(newItem);
    }

    const deleteLineupItem = async (lineupitem: LineupItem) => {
        const lineupitemsupdated = lineup.lineupItems.filter((x: LineupItem) => x.lineupItemId != lineupitem.lineupItemId)
        const newlineup = {
            lineupName: lineup.lineupName,
            lineupId: lineup.lineupId,
            lineupItems: lineupitemsupdated
        } as Lineup;

        updateLineup(newlineup);
    }

    const MoveLineupItem = async (lineupItem: LineupItem, dir: number) => {
        const sortedLineupItems = [...lineup.lineupItems].sort((a, b) => a.index - b.index);
        const currentIndex = sortedLineupItems.findIndex(i => i.lineupItemId === lineupItem.lineupItemId);

        if (currentIndex + dir >= lineup.lineupItems.length) return;
        const targetForSwap = sortedLineupItems[currentIndex + dir];

        [lineupItem.index, targetForSwap.index] = [targetForSwap.index, lineupItem.index];

        updateLineup({ ...lineup, lineupItems: sortedLineupItems })
    }

    return (
        <Stack direction="row" spacing={2}>
            <Box width="45%">
                <LineupTimeline selector={setLineupItemToEdit} add={AddLineupItem} remove={deleteLineupItem} lineupItems={lineup.lineupItems.sort((a, b) => a.index - b.index)} move={MoveLineupItem} startTime={new Date("December 17, 1995 " + lineup.startTime)} />
            </Box>

            <Box width="55%">
                <Paper style={{ padding: '1.2em', position: 'fixed', minWidth: "40%" }} sx={{ mt: "1.5em" }}>
                    <CustomForm title="Editing Lineup" save={saveLineup} initialValue={lineup} fields={lineupFields} />
                </Paper>
            </Box>
        </Stack>
            
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