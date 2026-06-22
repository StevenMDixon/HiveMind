import {  Container } from "@mui/material";
import Header from "@/components/Header";
import CustomTable, { type CellData } from "@/components/CustomTable";
import { useNavigate } from "react-router-dom";
import CustomDialog from '@/components/Dialog';

import { type CustomFormField } from '@/components/FormFields';

import type { Lineup } from '../types';

import { useQuery } from '@tanstack/react-query';
import { lineupsQueryOptions } from '../api/queryOptions';

import { useLineupCreateHook, useLineupDeleteHook } from '../hooks/lineupHooks';


const LineupView = () => {
    const { data: lineups, refetch: refectchLineups, isLoading } = useQuery(lineupsQueryOptions());

    const navigate = useNavigate();

    const { mutate: createLineup } = useLineupCreateHook();
    const { mutate: deleteLineup } = useLineupDeleteHook();

    const columns = [
        { key: 'lineupId', name: 'ID', align: 'left' },
        { key: 'lineupName', name: 'Name', align: 'left' }
    ] as CellData<Lineup>[];

    const actionColumns = [
        { key: 'a1', name: "Edit", align: 'center', action: (e: Lineup) => navigate("/lineups/" + e.lineupId), icon:"Edit"},
        { key: 'a2', name: "Delete", align: 'center', action: (e: Lineup) => handleDelete(e), icon:"Delete"}
    ] as CellData<Lineup>[];

    const lineupDefault = { lineupId: -1, lineupName: "", channelId: null, "startTime": "00:00:00" } as Lineup; 

    const handleDelete = async (lineup: Lineup) => {
        deleteLineup(lineup.lineupId)
    }

    const handleCreate = async (lineup: Lineup) => {
        createLineup(lineup);
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
            <CustomTable data={lineups} columns={columns} actionColumns={actionColumns} handleRetry={refectchLineups} isLoading={isLoading} />
        </Container>
    )
};

export default LineupView;
