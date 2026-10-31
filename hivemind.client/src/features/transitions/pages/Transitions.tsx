import Header from '@/components/Header';
import CustomTable, { type CellData, type PagingData } from '@/components/CustomTable';
import CustomDialog from '@/components/Dialog';

import Container from '@mui/material/Container';
import { useNavigate } from "react-router-dom";

import type { Transition } from '../types';

import {transitionDefault, fields } from '../fields'
import { useState } from 'react';
import { transitionQuerySearchOptions } from '../api/queryOptions';
import { useQuery } from '@tanstack/react-query';

import { useTransitionCreateHook, useTransitionDeleteHook } from '../hooks/transitionHooks';


const TransitionPage = () => {
 const { mutate: createTransition } = useTransitionCreateHook();
    const { mutate: deleteTransition } = useTransitionDeleteHook();

    const [search, setSearch] = useState('');
    const [pageIndex, setPageIndex] = useState(1);
    const [pageSize] = useState(18);

    const { data: transitions, refetch, isLoading } = useQuery(transitionQuerySearchOptions(search, pageIndex, pageSize));

    const navigate = useNavigate();

    const columns = [
        { key: 'id', name: 'ID', align: 'left' },
        { key: 'name', name: 'Transition Name', align: 'left' }
    ] as CellData<Transition>[];

    const actionColumns = [
        { key: 'a1', name: "Edit", action: (e: Transition) => navigate("/transitions/" + e.id), icon: "Edit" },
        { key: 'a2', name: "Delete", action: (e: Transition) => deleteTransition(e.id), icon: "Delete" }
    ] as CellData<Transition>[];

    const handleRetry = () => {
        refetch();
    };

    const Search = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setSearch(e.target.value);
    }
    
    const HandlePageIndexChange = (_: React.ChangeEvent<unknown>, value: number) => {
        setPageIndex(value);
    }

    const paging = {
        currentPage: pageIndex,
        dataTotal: transitions?.dataCount,
        pageSize: pageSize,
        changePage: HandlePageIndexChange
    } as PagingData;

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Transitions">
                <CustomDialog buttonText="Add Transition" title="Create Transtition" save={createTransition} initialValue={transitionDefault } fields={fields} />
            </Header>
            <CustomTable data={transitions?.data} handleRetry={handleRetry} columns={columns} actionColumns={actionColumns} isLoading={isLoading} searchFN={Search} paging={paging}/>
        </Container>
    )
}

export default TransitionPage;