import {  Container } from "@mui/material";
import Header from "@/components/Header";
import CustomTable, { type CellData, type PagingData } from "@/components/CustomTable";
import { useNavigate } from "react-router-dom";
import CustomDialog from '@/components/Dialog';
import { useState } from 'react';
import { type CustomFormField } from '@/components/FormFields';

import type { Lineup } from '../types';

import { useQuery } from '@tanstack/react-query';
import { lineupQuerySearchOptions } from '../api/queryOptions';

import { useLineupCreateHook, useLineupDeleteHook } from '../hooks/lineupHooks';


const LineupView = () => {

    const [search, setSearch] = useState('');
    const [pageIndex, setPageIndex] = useState(1);
    const [pageSize] = useState(18);

    const { data: lineups, refetch: refectchLineups, isLoading } = useQuery(lineupQuerySearchOptions(search, pageIndex, pageSize));

    const navigate = useNavigate();

    const { mutate: createLineup } = useLineupCreateHook();
    const { mutate: deleteLineup } = useLineupDeleteHook();

    const columns = [
        { key: 'id', name: 'ID', align: 'left' },
        { key: 'name', name: 'Name', align: 'left' },
        { key: 'startTime', name: 'Start Time', align: 'left' }
    ] as CellData<Lineup>[];

    const actionColumns = [
        { key: 'a1', name: "Edit", align: 'center', action: (e: Lineup) => navigate("/lineups/" + e.id), icon:"Edit"},
        { key: 'a2', name: "Delete", align: 'center', action: (e: Lineup) => handleDelete(e), icon:"Delete"}
    ] as CellData<Lineup>[];

    const lineupDefault = { id: -1, name: "", channelId: null, "startTime": "00:00:00" } as Lineup; 

    const handleDelete = async (lineup: Lineup) => {
        deleteLineup(lineup.id)
    }

    const handleCreate = async (lineup: Lineup) => {
        createLineup(lineup);
    }


    const Search = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setSearch(e.target.value);
    }
    
    const HandlePageIndexChange = (_: React.ChangeEvent<unknown>, value: number) => {
        console.log(value);
        setPageIndex(value);
    }

    const paging = {
        currentPage: pageIndex,
        dataTotal: lineups?.dataCount,
        pageSize: pageSize,
        changePage: HandlePageIndexChange
     } as PagingData;

    const fields = [
        { name: 'lineupName', Display: "Name", type: "Text", initialValue: lineupDefault.name },
        { name: 'startTime', display: "Start Time", type: "Text", initialValue: lineupDefault.startTime },
    ] as CustomFormField[];

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Lineups">
                <CustomDialog buttonText="Add Lineup" title={"Create Lineup"} save={handleCreate} initialValue={lineupDefault} fields={fields} />
            </Header>
            <CustomTable data={lineups?.data} columns={columns} actionColumns={actionColumns} handleRetry={refectchLineups} isLoading={isLoading} searchFN={Search} paging={paging}/>
        </Container>
    )
};

export default LineupView;
