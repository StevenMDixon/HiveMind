import { Container, AppBar, Toolbar, Typography, Stack, Box, Paper } from "@mui/material";
import { useParams } from "react-router-dom";
import { useState, Suspense } from 'react'
import ErrorBoundary from "../../Components/ErrorBoundary";

import ScheduleItemEditor from './Editor';
import ScheduleTimeline from './ScheduleTimeline';
import type { ScheduleItem } from './types';

const fetchScheduleData = async (id: number) => {
    const response = await fetch('/schedules/' + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch schedule: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
}

const ScheduleEdit = () => {
    const { id } = useParams();

    const [schedulePromise] = useState(() => fetchScheduleData(Number(id)))

    const [selectedScheduleItem, setSelectedScheduleItem] = useState <ScheduleItem | null>(null);

    const setScheduleItemToEdit = (e: ScheduleItem) => {
        setSelectedScheduleItem(e);
    }

    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Editing Schedule: {id}
                    </Typography>
                </Toolbar>
            </AppBar>
            <ErrorBoundary
                fallback={(error) => (
                    <p>{error.message}</p>
                )}>
                <Suspense fallback={<p> Loading </p>}>
                    <Stack direction="row" spacing={2}>
                        <Box width="40%">
                            <ScheduleTimeline selector={setScheduleItemToEdit} scheduleItemPromise={schedulePromise} />
                        </Box>
                        
                        <Box width="60%">
                            {selectedScheduleItem &&
                                <Paper style={{ padding: '1em', position: 'fixed', zIndex: 1000, minWidth: "50%" }} sx={{mt: "1em",} }>
                                    
                                 <ScheduleItemEditor scheduleItem={selectedScheduleItem} save={() => null} />
                                </Paper>
                            }
                        </Box>
                    </Stack>
                </Suspense>
            </ErrorBoundary>
        </Container>
    )
}

export default ScheduleEdit;


//sx = {{
//    position: 'fixed',
//        bottom: 0, // Positions the box at the bottom of the viewport
//            right: 0, // Positions the box at the right of the viewport
//                backgroundColor: 'primary.main',
//                    color: 'white',
//                        padding: 2, // Equivalent to theme.spacing(2)
//                            zIndex: 1000, // Ensures the box is on top of other content
//      }}