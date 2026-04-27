import {  Container } from "@mui/material";
import Header from "../Components/Header";
import { useState} from "react";
import CustomTable, { type CellData } from "../Components/Table";
import { useNavigate } from "react-router-dom";
import CustomDialog from '../Components/Dialog';
import { useGlobalNotification } from '../../Dashboard/useGlobalNotification';
import { type CustomFormField } from '../Components/FormFields';

import type { Lineup } from '../../Types/Lineup';

import { ApiClient } from '../../Api/ApiClient';

const LineupView = () => {
    const [lineupPromise, setLineupPromise] = useState(() => ApiClient.fetchLineups());
    const navigate = useNavigate();

    const { showNotification } = useGlobalNotification();

    const columns = [
        { key: 'lineupId', name: 'ID', align: 'left' },
        { key: 'lineupName', name: 'Name', align: 'left' }
    ] as CellData<Lineup>[];

    const actionColumns = [
        { key: 'a1', name: "Edit", align: 'center', action: (e: Lineup) => navigate("/lineups/" + e.lineupId), icon:"Edit"},
        { key: 'a2', name: "Delete", align: 'center', action: (e: Lineup) => handleDelete(e), icon:"Delete"}
    ] as CellData<Lineup>[];

    const handleRetry = () => {
        setLineupPromise(ApiClient.fetchLineups());
    };

    const lineupDefault = { lineupId: -1, lineupName: "", channelId: null, "startTime": "00:00:00" } as Lineup; 

    const handleDelete = async (lineup: Lineup) => {
        const result = await ApiClient.deleteLineup(lineup);
        setLineupPromise(ApiClient.fetchLineups());

        if (result.ok) {
            showNotification("Lineup Deleted", "success");
        } else {
            showNotification("Failed to delete lineup", "error");
        }
    }

    const handleCreate = async (lineup: Lineup) => {
        const result = await ApiClient.createLineup(lineup);

        if (result.ok) {
            showNotification("Lineup Created", "success");
        } else {
            showNotification("Failed to create lineup", "error");
        }

        setLineupPromise(ApiClient.fetchLineups());
    }

    const fields = [
        { name: 'lineupName', Display: "Name", type: "Text", initialValue: lineupDefault.lineupName },
        { name: 'startTime', display: "Start Time", type: "Text", initialValue: lineupDefault.startTime },
    ] as CustomFormField[];

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Lineups">
                <CustomDialog buttonText="Add Lineup" title={"Create Lineup"} save={handleCreate} initialValue={lineupDefault} fields={fields} />
            </Header>
            <CustomTable dataPromise={lineupPromise} columns={columns} actionColumns={actionColumns} handleRetry={handleRetry} />
        </Container>
    )
};

export default LineupView;
