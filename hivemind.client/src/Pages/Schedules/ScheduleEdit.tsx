import { Container, AppBar, Toolbar, Typography, Stack, Box, Paper } from "@mui/material";
import { useParams, useNavigate } from "react-router-dom";
import { useState, Suspense, use,  } from 'react'
import ErrorBoundary from "../../Components/ErrorBoundary";

import ScheduleTimeline from './ScheduleTimeline';
import type { ScheduleItem, Schedule } from '../../Types/Schedule';
import type { Channel } from '../../Types/Channel';

import CustomForm from '../Components/CustomForm';
import { type CustomFormField } from '../Components/FormFields';

import { ApiClient } from '../../Api/ApiClient';
import { toEnumOptions } from "../../Utilities/FormOptionsMapper"

import { useGlobalNotification } from '../../Dashboard/useGlobalNotification';


const ScheduleDataWrapper = ({ schedulePromise, channelPromise }: { schedulePromise: Promise<Schedule>, channelPromise: Promise<Channel[]>}) => {
    const scheduleData = use(schedulePromise);
    const availableChannels = use(channelPromise);

    const [schedule, setSchedule] = useState(scheduleData);
    const [scheduleItems, setScheduleItems] = useState<ScheduleItem[]>(schedule.scheduleItems);

    const navigate = useNavigate();
    const { showNotification } = useGlobalNotification();

    const setScheduleItemToEdit = (e: ScheduleItem) => {
        navigate("/schedules/" + schedule.scheduleId + "/items/" + e.scheduleItemId)
    }

    const deleteScheduleItem = async (scheduleItem: ScheduleItem) => {
        const scheduleItemsUpdated = scheduleItems.filter(x => x.scheduleItemId != scheduleItem.scheduleItemId)

        const newSchedule = {
            scheduleName: schedule.scheduleName,
            scheduleId: schedule.scheduleId,
            scheduleItems: scheduleItemsUpdated
        } as Schedule;

        const result = await ApiClient.updateSchedule(newSchedule);

        if (result.ok) {
            showNotification("Schedule Item Deleted", "success");
            setScheduleItems(scheduleItemsUpdated)
        } else {
            showNotification("Failed to Delete Schedule Item", "error");
        }
    }

    const AddScheduleItem = async () => {
        const currentIdx = (scheduleItems.length > 0 ? scheduleItems[scheduleItems.length -1].index : 0) + 1;

        const newItem = {
            scheduleId: schedule.scheduleId,
            scheduleItemId: -currentIdx,
            name: 'New ' + currentIdx,
            index: currentIdx,
            type: 'Generic',
            queries: []
        } as ScheduleItem;

        const result = await ApiClient.createScheduleItem(newItem);

        if (result.ok) {
            const id = await result.json();
            newItem.scheduleItemId = id;

            const updatedItems = [...scheduleItems, newItem];
            setScheduleItems(updatedItems);

            showNotification("Schedule Item Added", "success");
        } else {
            showNotification("Failed to Add Schedule Item", "error");
        }
        
    }

    const MoveScheduleItem = async (scheduleItem: ScheduleItem, dir: number) => {
        const sortedScheduleItems = [...scheduleItems].sort((a, b) => a.index - b.index);
        const currentIndex = sortedScheduleItems.findIndex(i => i.scheduleItemId === scheduleItem.scheduleItemId); 

        if (currentIndex + dir >= scheduleItems.length) return;
        const targetForSwap = sortedScheduleItems[currentIndex + dir];
        
        [scheduleItem.index, targetForSwap.index] = [targetForSwap.index, scheduleItem.index];

        setScheduleItems(sortedScheduleItems);

        const result = await ApiClient.updateSchedule({ ...schedule, scheduleItems: sortedScheduleItems })

        if (result.ok) {
            showNotification("Schedule Item Moved", "success");
            
        } else {
            showNotification("Failed to Save Schedule", "error");
        }
    }

    const saveSchedule = async (editedSchedule: Schedule) => {
        editedSchedule.scheduleItems = scheduleItems;
        const result = await ApiClient.updateSchedule(editedSchedule);

        if (result.ok) {
            showNotification("Schedule Saved", "success");
            setSchedule(editedSchedule)
        } else {
            showNotification("Failed to Save Schedule", "error");
        }

    }

    const scheduleFields = [
        { name: 'scheduleName', display: "Name", type: "Text", initialValue: schedule.scheduleName },
        { name: 'channelId', display: "Assigned Channel", type: "Select", initialValue: schedule.channelId, options: toEnumOptions(availableChannels, "channelId", "channelName",true) },
        { name: 'startTime', display: "Start Time", type: "Text", initialValue: schedule.startTime ?? ""}
    ] as CustomFormField[];

    return (
            <Stack direction="row" spacing={2}>
                <Box width="45%">
                <ScheduleTimeline selector={setScheduleItemToEdit} add={AddScheduleItem} remove={deleteScheduleItem} scheduleItems={scheduleItems.sort((a, b) => a.index - b.index)} move={MoveScheduleItem} startTime={new Date("December 17, 1995 " + schedule.startTime)} />
                </Box>

                <Box width="55%">
                    <Paper style={{ padding: '1.2em', position: 'fixed', minWidth: "40%" }} sx={{ mt: "1.5em" }}>
                        <CustomForm title="Editing Schedule" save={saveSchedule} initialValue={schedule} fields={scheduleFields} />
                    </Paper>
                </Box>
            </Stack>
      )
}

const ScheduleEdit = () => {
    const { id } = useParams();

    const [schedulePromise] = useState(() => ApiClient.fetchScheduleData(Number(id)));
    const [channelPromise] = useState(() => ApiClient.fetchChannels());
   
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
                    <ScheduleDataWrapper schedulePromise={schedulePromise} channelPromise={channelPromise} />
                </Suspense>
            </ErrorBoundary>
        </Container>
    )
}

export default ScheduleEdit;