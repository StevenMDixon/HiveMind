import { Container, AppBar, Toolbar, Typography, Stack, Box, Paper } from "@mui/material";
import { useParams, useNavigate } from "react-router-dom";
import { useState, Suspense, use,  } from 'react'
import ErrorBoundary from "../../Components/ErrorBoundary";

import LineupTimeline from './LineupTimeline';
import type { LineupItem, Lineup } from '../../Types/Lineup';

import CustomForm from '../Components/CustomForm';
import { type CustomFormField } from '../Components/FormFields';

import { ApiClient } from '../../Api/ApiClient';

import { useGlobalNotification } from '../../Dashboard/useGlobalNotification';


const LineupDataWrapper = ({ lineupPromise}: { lineupPromise: Promise<Lineup>}) => {
    const lineupData = use(lineupPromise);

    const [lineup, setLineup] = useState(lineupData);
    const [lineupItems, setLineupItems] = useState<LineupItem[]>(lineup.lineupItems);

    const navigate = useNavigate();
    const { showNotification } = useGlobalNotification();

    const setLineupItemToEdit = (e: LineupItem) => {
        navigate("/lineups/" + lineup.lineupId + "/items/" + e.lineupItemId)
    }

    const deleteLineupItem = async (lineupItem: LineupItem) => {
        const lineupItemsUpdated = lineupItems.filter(x => x.lineupItemId != lineupItem.lineupItemId)
        const newLineup = {
            lineupName: lineup.lineupName,
            lineupId: lineup.lineupId,
            lineupItems: lineupItemsUpdated
        } as Lineup;

        const result = await ApiClient.updateLineup(newLineup);

        if (result.ok) {
            showNotification("Lineup Item Deleted", "success");
            setLineupItems(lineupItemsUpdated)
        } else {
            showNotification("Failed to Delete Lineup Item", "error");
        }
    }

    const AddLineupItem = async () => {
        const currentIdx = (lineupItems.length > 0 ? lineupItems[lineupItems.length -1].index : 0) + 1;

        const newItem = {
            lineupId: lineup.lineupId,
            lineupItemId: -currentIdx,
            name: 'New ' + currentIdx,
            index: currentIdx,
            type: 'Generic',
            queries: []
        } as LineupItem;

        const result = await ApiClient.createLineupItem(newItem);

        if (result.ok) {
            const id = await result.json();
            newItem.lineupItemId = id;
            const updatedItems = [...lineupItems, newItem];
            setLineupItems(updatedItems);

            showNotification("Lineup Item Added", "success");
        } else {
            showNotification("Failed to Add Lineup Item", "error");
        }
        
    }

    const MoveLineupItem = async (lineupItem: LineupItem, dir: number) => {
        const sortedLineupItems = [...lineupItems].sort((a, b) => a.index - b.index);
        const currentIndex = sortedLineupItems.findIndex(i => i.lineupItemId === lineupItem.lineupItemId); 

        if (currentIndex + dir >= lineupItems.length) return;
        const targetForSwap = sortedLineupItems[currentIndex + dir];
        
        [lineupItem.index, targetForSwap.index] = [targetForSwap.index, lineupItem.index];

        setLineupItems(sortedLineupItems);
        const result = await ApiClient.updateLineup({ ...lineup, lineupItems: sortedLineupItems })

        if (result.ok) {
            showNotification("Lineup Item Moved", "success");
            
        } else {
            showNotification("Failed to Move Lineup Item", "error");
        }
    }

    const saveLineup = async (editedLineup: Lineup) => {
        editedLineup.lineupItems = lineupItems;
        const result = await ApiClient.updateLineup(editedLineup);

        if (result.ok) {
            showNotification("Lineup Saved", "success");
            setLineup(editedLineup)
        } else {
            showNotification("Failed to Save Lineup", "error");
        }

    }

    const lineupFields = [
        { name: 'lineupName', display: "Name", type: "Text", initialValue: lineup.lineupName },
        { name: 'startTime', display: "Start Time", type: "Text", initialValue: lineup.startTime ?? ""}
    ] as CustomFormField[];

    return (
            <Stack direction="row" spacing={2}>
                <Box width="45%">
                <LineupTimeline selector={setLineupItemToEdit} add={AddLineupItem} remove={deleteLineupItem} lineupItems={lineupItems.sort((a, b) => a.index - b.index)} move={MoveLineupItem} startTime={new Date("December 17, 1995 " + lineup.startTime)} />
                </Box>

                <Box width="55%">
                    <Paper style={{ padding: '1.2em', position: 'fixed', minWidth: "40%" }} sx={{ mt: "1.5em" }}>
                        <CustomForm title="Editing Lineup" save={saveLineup} initialValue={lineup} fields={lineupFields} />
                    </Paper>
                </Box>
            </Stack>
      )
}

const LineupEdit = () => {
    const { id } = useParams();

    const [lineupPromise] = useState(() => ApiClient.fetchLineup(Number(id)));
   
    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Editing Lineup: {id}
                    </Typography>
                </Toolbar>
            </AppBar>
            <ErrorBoundary
                fallback={(error) => (
                    <p>{error.message}</p>
                )}>
                <Suspense fallback={<p> Loading </p>}>
                    <LineupDataWrapper lineupPromise={lineupPromise} />
                </Suspense>
            </ErrorBoundary>
        </Container>
    )
}

export default LineupEdit;