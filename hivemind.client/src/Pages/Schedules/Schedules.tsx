import {  Container } from "@mui/material";
import Header from "../Components/Header";
import { useState} from "react";
import CustomTable, { type CellData } from "../Components/Table";
import { useNavigate } from "react-router-dom";

interface Schedule {
    scheduleId: number;
    scheduleName: string;
}

// Function that creates a new promise with error handling
const fetchSchedules = async (): Promise<Schedule[]> => {
    const response = await fetch('/schedules');

    if (!response.ok) {
        throw new Error(`Failed to fetch schedules: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data.schedules;
};

const Schedules = () => {
    const [schedulePromise, setSchedulePromise] = useState(() => fetchSchedules());
    const navigate = useNavigate();

    const columns = [
        { key: 'scheduleId', name: 'ID', align: 'left' },
        { key: 'scheduleName', name: 'Name', align: 'left' }
    ] as CellData[];

    const actionColumns = [
        { key: 'a1', name: "Action", align: 'center', action: (e: Schedule) => navigate("/schedules/" + e.scheduleId)}
    ] as CellData[];

    const handleRetry = () => {
        setSchedulePromise(fetchSchedules());
    };

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Schedules">
            </Header>
            <CustomTable dataPromise={schedulePromise} columns={columns} actionColumns={actionColumns} handleRetry={handleRetry} />
        </Container>
    )
};

export default Schedules;
