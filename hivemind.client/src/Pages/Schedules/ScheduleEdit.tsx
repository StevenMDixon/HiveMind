import { Container, AppBar, Toolbar, Typography, Stack, Box, Paper } from "@mui/material";
import { useParams } from "react-router-dom";
import { useState, Suspense, use,  } from 'react'
import ErrorBoundary from "../../Components/ErrorBoundary";

import ScheduleTimeline from './ScheduleTimeline';
import CollectionScheduleEditor from './ScheduleItemCollectionEditor';
import type { ScheduleItem, Schedule, CollectionScheduleItem } from '../../Types/Schedule';

import CustomForm from '../Components/CustomForm';
import { type CustomFormField } from '../Components/FormFields';

import { fetchScheduleData, updateSchedule, createScheduleItem, fetchCollectionScheduleItemsByScheduleItem } from '../../Api/Schedules';
import { fetchCollections } from '../../Api/Collections';
import { fetchCollectionTypes, fetchPlayoutTypes } from '../../Api/Enums';

const ScheduleDataWrapper = ({ schedulePromise }: { schedulePromise: Promise<Schedule>}) => {
    const schedule = use(schedulePromise);

    const [scheduleItems, setScheduleItems] = useState<ScheduleItem[]>(schedule.scheduleItems);

    const [availableCollections] = useState(() => fetchCollections());
    const [availablePlayoutTypes] = useState(() => fetchPlayoutTypes());
    const [availableCollectionTypes] = useState(() => fetchCollectionTypes());

    const [selectedScheduleItem, setSelectedScheduleItem] = useState<ScheduleItem | null>(null);

    const [collectionScheduleItems, setCollectionScheduleItem] = useState<Promise<CollectionScheduleItem[]> | null | undefined>();

    const setScheduleItemToEdit = (e: ScheduleItem) => {
        setSelectedScheduleItem(e);
        setCollectionScheduleItem(() => fetchCollectionScheduleItemsByScheduleItem(e.scheduleItemId));
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

        const newSchedule = {
            scheduleName: schedule.scheduleName,
            scheduleId: schedule.scheduleId,
            scheduleItems: scheduleItemsUpdated
        } as Schedule;

        updateSchedule(newSchedule);
        setScheduleItems(scheduleItemsUpdated)
    }

    const deleteScheduleItem = (scheduleItem: ScheduleItem) => {

        const scheduleItemsUpdated = scheduleItems.filter(x => x.scheduleItemId != scheduleItem.scheduleItemId)

        const newSchedule = {
            scheduleName: schedule.scheduleName,
            scheduleId: schedule.scheduleId,
            scheduleItems: scheduleItemsUpdated
        } as Schedule;

        updateSchedule(newSchedule);
        setScheduleItems(scheduleItemsUpdated)
        setSelectedScheduleItem(null);
    }

    const AddScheduleItem = async () => {
        const currentIdx = scheduleItems.length + 1;

        const newItem = {
            scheduleId: schedule.scheduleId,
            scheduleItemId: -currentIdx,
            name: 'New ' + currentIdx,
            index: currentIdx,
            type: 'Generic',
            collections: []
        } as ScheduleItem;

        const id = await createScheduleItem(newItem);
        newItem.scheduleItemId = id;

        const updatedItems = [...scheduleItems, newItem];
        setScheduleItems(updatedItems);
    }

    const scheduleItemFields = [
        { name: 'name', type: "Text", initialValue: "" },
        { name: 'index', type: "Text", initialValue: 0 },
        { name: 'type', type: "Radio", initialValue: "Generic", options: ["Generic", "Block"] }
    ] as CustomFormField[];

    return (
            <Stack direction="row" spacing={2}>
                <Box width="40%">
                    <ScheduleTimeline selector={setScheduleItemToEdit} add={AddScheduleItem} remove={deleteScheduleItem} scheduleItems={scheduleItems} />
                </Box>

                <Box width="60%">
                    {selectedScheduleItem &&
                        <Paper style={{ padding: '1.2em', position: 'fixed', minWidth: "30%" }} sx={{ mt: "1.5em" }}>
                        <CustomForm key={selectedScheduleItem.scheduleItemId}  title="Editing Schedule Item" save={updateScheduleItem} initialValue={selectedScheduleItem} fields={scheduleItemFields}>
                        <Suspense>
                                <CollectionScheduleEditor
                                    key={selectedScheduleItem.scheduleItemId}
                                    scheduleItem={selectedScheduleItem}
                                    collectionScheduleItemsPromise={collectionScheduleItems}
                                    availableCollectionPromise={availableCollections}
                                    collectionTypePromise={availableCollectionTypes}
                                    playoutTypePromise={availablePlayoutTypes} />
                        </Suspense>
                        </CustomForm>
                        </Paper>
                    }
                </Box>
            </Stack>
      )
}

const ScheduleEdit = () => {
    const { id } = useParams();

    const [schedulePromise] = useState(() => fetchScheduleData(Number(id)));
   
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
                    <ScheduleDataWrapper schedulePromise={schedulePromise}/>
                </Suspense>
            </ErrorBoundary>
        </Container>
    )
}

export default ScheduleEdit;