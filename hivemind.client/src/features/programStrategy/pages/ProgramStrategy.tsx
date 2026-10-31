import Container from '@mui/material/Container';
import { useNavigate } from "react-router-dom";

import Header from '@/components/Header';
import CustomTable, { type CellData, type PagingData } from '@/components/CustomTable';
import { type CustomFormField } from '@/components/FormFields';
import CustomDialog from '@/components/Dialog';
import { useState } from 'react';
import type { ProgramStrategy } from '../types';

import { programStrategyQuerySearchOptions } from '../api/queryOptions';
import { useQuery } from '@tanstack/react-query';

import { useProgramStrategyCreateHook, useProgramStrategyDeleteHook } from '../hooks/programStrategyHooks';

const ProgramStrategyPage = () => {

    const [search, setSearch] = useState('');
    const [pageIndex, setPageIndex] = useState(1);
    const [pageSize] = useState(18);

    const { data: programStrategies, refetch: refetchProgramStrategies, isLoading } = useQuery(programStrategyQuerySearchOptions(search, pageIndex, pageSize));

    const { mutate: createProgram } = useProgramStrategyCreateHook();
    const { mutate: deleteProgram } = useProgramStrategyDeleteHook();

    const navigate = useNavigate();

    const columns = [
        { key: 'id', name: 'ID', align: 'left' },
        { key: 'name', name: 'Name', align: 'left' },
        { key: 'advancedDays', name: 'Advanced Days', align: 'left' },
        { key: 'active', name: 'Active', align: 'left' },
    ] as CellData<ProgramStrategy>[];

    const actionColumns = [
        { key: 'a1', name: "Edit", align: 'center', action: (e) => navigate("/programstrategy/" + e.id), icon: "Edit" },
        { key: 'a2', name: "Delet", align: 'center', action: (e) => handleDelete(e), icon: "Delete" },
    ] as CellData<ProgramStrategy>[];

    const programDefault = { id: -1, name: "", active: false, advancedDays: 0 } as ProgramStrategy; 

    const handleDelete = async (program: ProgramStrategy) => {
        deleteProgram(program.id)
    }

    const handleCreate = async (program: ProgramStrategy) => {
        createProgram(program);
    }

    const fields = [
        { name: 'name', display: "Name", type: "Text", initialValue: programDefault.name },
        { name: 'advancedDays', display: "Advanced Days", type: "Text", initialValue: programDefault.advancedDays }
    ] as CustomFormField[];


    const Search = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setSearch(e.target.value);
    }
    
    const HandlePageIndexChange = (_: React.ChangeEvent<unknown>, value: number) => {
        console.log(value);
        setPageIndex(value);
    }

    const paging = {
        currentPage: pageIndex,
        dataTotal: programStrategies?.dataCount,
        pageSize: pageSize,
        changePage: HandlePageIndexChange
     } as PagingData;   


    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Program Strategies">
                <CustomDialog buttonText="Add Program" title={"Create Program Strategy"} save={handleCreate} initialValue={programDefault} fields={fields} />
            </Header>
            <CustomTable data={programStrategies?.data} columns={columns} actionColumns={actionColumns} handleRetry={refetchProgramStrategies} isLoading={isLoading} searchFN={Search} paging={paging}></CustomTable>
        </Container>
    )
}

export default ProgramStrategyPage;