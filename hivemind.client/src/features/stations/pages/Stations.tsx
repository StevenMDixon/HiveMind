import Header from '@/components/Header';
import CustomTable, { type CellData } from '@/components/CustomTable';
import CustomDialog from '@/components/Dialog';

import Container from '@mui/material/Container';
import { useNavigate } from "react-router-dom";

import type { Station } from '../types';

import {stationDefault, fields } from '../fields'

import { stationsQueryOptions } from '../api/queryOptions';
import { useQuery } from '@tanstack/react-query';

import { useStationCreateHook, useStationDeleteHook } from '../hooks/stationHooks';

const StationPage = () => {
    const { mutate: createStation } = useStationCreateHook();
    const { mutate: deleteStation } = useStationDeleteHook();

    const { data, refetch, isLoading } = useQuery(stationsQueryOptions());

    const navigate = useNavigate();

    const columns = [
        { key: 'id', name: 'ID', align: 'left' },
        { key: 'name', name: 'Station Name', align: 'left' },
        { key: 'number', name: 'Station Number', align: 'left' }
    ] as CellData<Station>[];

    const actionColumns = [
        { key: 'a1', name: "Edit", action: (e: Station) => navigate("/stations/" + e.id), icon: "Edit" },
        { key: 'a2', name: "Delete", action: (e: Station) => deleteStation(e.id), icon: "Delete" }
    ] as CellData<Station>[];

    const handleRetry = () => {
        refetch();
    };

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Stations">
                <CustomDialog buttonText="Add Station" title="Create Station" save={createStation} initialValue={stationDefault } fields={fields} />
            </Header>
            {data && <CustomTable data={data} handleRetry={handleRetry} columns={columns} actionColumns={actionColumns} isLoading={isLoading} />}
        </Container>
    )
}

export default StationPage;