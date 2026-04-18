import {  Container } from "@mui/material";
import Header from "../Components/Header";
import { useState} from "react";
import CustomTable, { type CellData } from "../Components/Table";
import { useNavigate } from "react-router-dom";
import CustomDialog from '../Components/Dialog';
import { useGlobalNotification } from '../../Dashboard/useGlobalNotification';
import { type CustomFormField } from '../Components/FormFields';

import type { Schedule } from '../../Types/Schedule';

import { ApiClient } from '../../Api/ApiClient';

const Schedules = () => {
    const [schedulePromise, setSchedulePromise] = useState(() => ApiClient.fetchSchedules());
    const navigate = useNavigate();

    const { showNotification } = useGlobalNotification();

    const columns = [
        { key: 'scheduleId', name: 'ID', align: 'left' },
        { key: 'scheduleName', name: 'Name', align: 'left' },
        { key: 'channelId', name: 'Assigned Channel ID', align: 'left' }
    ] as CellData<Schedule>[];

    const actionColumns = [
        { key: 'a1', name: "Edit", align: 'center', action: (e: Schedule) => navigate("/schedules/" + e.scheduleId), icon:"Edit"},
        { key: 'a2', name: "Delete", align: 'center', action: (e: Schedule) => handleDelete(e), icon:"Delete"}
    ] as CellData<Schedule>[];

    const handleRetry = () => {
        setSchedulePromise(ApiClient.fetchSchedules());
    };

    const scheduleDefault = { scheduleId: -1, scheduleName: "", channelId: null, "startTime": "00:00:00" } as Schedule; 

    const handleDelete = async (schedule: Schedule) => {
        const result = await ApiClient.deleteSchedule(schedule);
        setSchedulePromise(ApiClient.fetchSchedules());

        if (result.ok) {
            showNotification("Schedule Deleted", "success");
        } else {
            showNotification("Failed to delete schedule", "error");
        }
    }

    const handleCreate = async (schedule: Schedule) => {
        const result = await ApiClient.createSchedule(schedule);

        if (result.ok) {
            showNotification("Schedule Created", "success");
        } else {
            showNotification("Failed to create schedule", "error");
        }

        setSchedulePromise(ApiClient.fetchSchedules());
    }

    const fields = [
        { name: 'scheduleName', Display: "Name", type: "Text", initialValue: scheduleDefault.scheduleName },
        { name: 'channelId', display: "Assigned Channel", type: "Text", initialValue: scheduleDefault.channelId, required: false },
        { name: 'startTime', display: "Start Time", type: "Text", initialValue: scheduleDefault.startTime },
    ] as CustomFormField[];

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Schedules">
                <CustomDialog buttonText="Add Schedule" title={"Create Schedule"} save={handleCreate} initialValue={scheduleDefault} fields={fields} />
            </Header>
            <CustomTable dataPromise={schedulePromise} columns={columns} actionColumns={actionColumns} handleRetry={handleRetry} />
        </Container>
    )
};

export default Schedules;
