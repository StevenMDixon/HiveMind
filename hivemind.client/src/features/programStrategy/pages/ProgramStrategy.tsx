import Container from '@mui/material/Container';
import { useNavigate } from "react-router-dom";

import Header from '@/components/Header';
import CustomTable, { type CellData } from '@/components/CustomTable';
import { type CustomFormField } from '@/components/FormFields';
import CustomDialog from '@/components/Dialog';

import type { ProgramStrategy } from '../types';

import { programStrategyQueryOptions } from '../api/queryOptions';
import { useQuery } from '@tanstack/react-query';

import { useProgramStrategyCreateHook, useProgramStrategyDeleteHook } from '../hooks/programStrategyHooks';

const ProgramStrategyPage = () => {

    const { data: programStrategies, refetch: refetchProgramStrategies, isLoading } = useQuery(programStrategyQueryOptions());

    const { mutate: createProgram } = useProgramStrategyCreateHook();
    const { mutate: deleteProgram } = useProgramStrategyDeleteHook();

    const navigate = useNavigate();

    const columns = [
        { key: 'programStrategyId', name: 'ID', align: 'left' },
        { key: 'name', name: 'Name', align: 'left' },
        { key: 'advancedDays', name: 'Advanced Days', align: 'left' },
        { key: 'active', name: 'Active', align: 'left' },
        { key: 'startDate', name: 'Start Date', align: 'left' },
        {key: 'endDate', name: 'End Date', align: 'left' }
    ] as CellData<ProgramStrategy>[];

    const actionColumns = [
        { key: 'a1', name: "Edit", align: 'center', action: (e) => navigate("/programstrategy/" + e.programStrategyId), icon: "Edit" },
        { key: 'a2', name: "Delet", align: 'center', action: (e) => handleDelete(e), icon: "Delete" },

    ] as CellData<ProgramStrategy>[];

    const programDefault = { programStrategyId: -1, name: "", active: false, advancedDays: 0 } as ProgramStrategy; 

    const handleDelete = async (program: ProgramStrategy) => {
        deleteProgram(program.programStrategyId)
    }

    const handleCreate = async (program: ProgramStrategy) => {
        createProgram(program);
    }

    const fields = [
        { name: 'name', display: "Name", type: "Text", initialValue: programDefault.name },
        { name: 'advancedDays', display: "Advanced Days", type: "Text", initialValue: programDefault.advancedDays }
    ] as CustomFormField[];

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Program Strategies">
                <CustomDialog buttonText="Add Program" title={"Create Program Strategy"} save={handleCreate} initialValue={programDefault} fields={fields} />
            </Header>
            <CustomTable data={programStrategies} columns={columns} actionColumns={actionColumns} handleRetry={refetchProgramStrategies} isLoading={isLoading}></CustomTable>
        </Container>
    )
}

export default ProgramStrategyPage;