import { Container, AppBar, Toolbar, Typography, Stack, Box, Paper } from "@mui/material";
import { useParams } from "react-router-dom";
import { useState, Suspense, use } from 'react'
import ErrorBoundary from "../../Components/ErrorBoundary";

import ScheduleTimeline from './ScheduleTimeline';
import type { ScheduleItem, Schedule, CollectionScheduleItem } from '../../Types/Schedule';

import CustomForm from '../Components/CustomForm';
import { type CustomFormField } from '../Components/FormFields';

import { fetchScheduleData, updateSchedule } from '../../Api/Schedules';


const ScheduleDataWrapper = ({ schedulePromise }: { schedulePromise: Promise<Schedule> }) => {
    const schedule = use(schedulePromise);

    const [scheduleItems, setScheduleItems] = useState<ScheduleItem[]>(schedule.scheduleItems);

    const [selectedScheduleItem, setSelectedScheduleItem] = useState<ScheduleItem | null>(null);

    const setScheduleItemToEdit = (e: ScheduleItem) => {
        setSelectedScheduleItem(e);
    }

    const updateScheduleItem = (scheduleItem: ScheduleItem) => {

        const scheduleItemsUpdated = [...scheduleItems];

        scheduleItemsUpdated.forEach(item => {
            if (item.scheduleItemId == scheduleItem.scheduleItemId) {
                item.name = scheduleItem.name;
                item.index = scheduleItem.index;
                item.type = scheduleItem.type;
            }
        });

        setScheduleItems(scheduleItemsUpdated);

        const newSchedule = {
            scheduleName: schedule.scheduleName,
            scheduleId: schedule.scheduleId,
            scheduleItems: scheduleItemsUpdated
        } as Schedule;

        updateSchedule(newSchedule);
    }

    const AddScheduleItem = () => {
        const currentIdx = scheduleItems.length + 1;
        setScheduleItems([
            ...scheduleItems,
            {
                scheduleId: schedule.scheduleId,
                scheduleItemId: -currentIdx,
                name: 'New ' + currentIdx,
                index: currentIdx,
                type: 'Generic',
                collections: []
            } as ScheduleItem
        ])
    }

    const scheduleItemFields = [
        { name: 'name', type: "Text", initialValue: "" },
        { name: 'index', type: "Text", initialValue: 0 },
        { name: 'type', type: "Radio", initialValue: "Generic", options: ["Generic", "Block"] }
    ] as CustomFormField[];

    return (
            <Stack direction="row" spacing={2}>
                <Box width="40%">
                <ScheduleTimeline selector={setScheduleItemToEdit} add={AddScheduleItem} scheduleItems={scheduleItems} />
                </Box>

                <Box width="60%">
                    {selectedScheduleItem &&
                        <Paper style={{ padding: '1em', position: 'fixed', zIndex: 1000, minWidth: "50%" }} sx={{ mt: "1em", }}>
                        <CustomForm title="Editing Schedule Item" save={updateScheduleItem} initialValue={selectedScheduleItem} fields={scheduleItemFields}>
                            <ScheduleItemCollectionsEdit scheduleItemsCollections={selectedScheduleItem.collections} />
                        </CustomForm>
                        </Paper>
                    }
                </Box>
            </Stack>
      )
}

const ScheduleItemCollectionsEdit = ({ scheduleItemsCollections }: { scheduleItemsCollections: CollectionScheduleItem[] }) => {

    return (
        <div>
        { scheduleItemsCollections.map((item, index) => (
            <p key={index}>Collection Item ID: {item.collectionScheduleItemId}</p>
        ))}
        </div>
      )
}

const ScheduleEdit = () => {
    const { id } = useParams();

    const [schedulePromise] = useState(() => fetchScheduleData(Number(id)))

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
                    <ScheduleDataWrapper schedulePromise={schedulePromise} />
                </Suspense>
            </ErrorBoundary>
        </Container>
    )
}

export default ScheduleEdit;