import Container from '@mui/material/Container';
import { IconButton, Stack } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';
import Typography from '@mui/material/Typography';

import CustomForm from "@/components/CustomForm";
import { type CustomFormField } from '@/components/FormFields';
import { type ProgramStrategy, type ProgramStrategyLineup } from '../types';

import { useProgramStrategyUpdateHook } from '../hooks/programStrategyHooks';

import { useState } from 'react'
import type { EnumOptionsObj } from '@/utilities/types';

import EditingGrid, { type EditingGridColumns } from '@/components/EditingGrid';
import { lineupSelectionTypesQueryOptions, lineupsQueryOptions } from '../api/queryOptions'
import { useQuery } from '@tanstack/react-query';


interface ProgramStrategyLayoutProps {
    strategy: ProgramStrategy;
}

const ProgramStrategyLayout = ({ strategy }: ProgramStrategyLayoutProps) => {

    const { mutate: saveStrategy } = useProgramStrategyUpdateHook();

    const { data: selectionTypes, isLoading: lineupTypesLoading } = useQuery(lineupSelectionTypesQueryOptions());
    const { data: availableLineups, isLoading: lineupsLoading } = useQuery(lineupsQueryOptions());

    const [lineups, setLineupsData] = useState<ProgramStrategyLineup[]>(strategy.programStrategyItems ?? []);
    
    const mappedLineups = availableLineups?.reduce((acc: EnumOptionsObj, cur) => { acc[cur.lineupId] = cur.lineupName; return acc; }, {});

    const formatLineup = (item: number) => {
        return mappedLineups ? mappedLineups[item] : item;
    }

    const formatTypes = (item: number) => {
        return selectionTypes ? selectionTypes[item] : item;
    }

    const strategyFields = [
        { name: 'active', display: "Active", type: "Switch", initialValue: strategy.active },
        { name: 'name', display: "Name", type: "Text", initialValue: strategy.name },
        { name: 'advancedDays', display: "Days to Schedule", type: "Number", initialValue: strategy.advancedDays },
        { name: 'startDate', display: "Start Date", type: "Text", initialValue: strategy.startDate },
        { name: 'endDate', display: "End Date", type: "Text", initialValue: strategy.endDate }

    ] as CustomFormField[];

    const saveProgramStrategy = (e: ProgramStrategy) => {
        const newData = {
            programStrategyId: e.programStrategyId,
            name: e.name,
            startDate: e.startDate,
            endDate: e.endDate,
            active: e.active,
            advancedDays: e.advancedDays,
            programStrategyItems: lineups
        } as ProgramStrategy

        console.log(newData);

        saveStrategy(newData);
    }

    const removeLineup = (item: ProgramStrategyLineup) => {
        const newFilterData = lineups.filter(x => x.programStrategyLineupId != item.programStrategyLineupId)
        setLineupsData(newFilterData)
    }

    const addLineup = () => {
        const newFilterData = [...lineups, { programStrategyLineupId: (lineups.length + 1) * -1, lineUpId: 0, selectionType: 0, selectionOption: "" } as ProgramStrategyLineup];
        setLineupsData(newFilterData);
    }

    const saveLineup = (item: ProgramStrategyLineup) => {
        setLineupsData(lineups.map(lineup => lineup.programStrategyLineupId == item.programStrategyLineupId ? item : lineup));
    }

    const gridColumns = [
        { name: 'lineUpId', display: 'Line Up', initialValue: 0, type: 'Select', format: formatLineup, options: mappedLineups },
        { name: 'selectionType', display: 'Type', initialValue: 0, type: 'Select', format: formatTypes, options: selectionTypes },
        { name: 'selectionOption', display: 'Option', initialValue: 0, type: 'Text' }
    ] as EditingGridColumns<ProgramStrategyLineup>[]

    return (
        <Container sx={{ mt: 5 }}>
            <CustomForm title="Editing Program Strategy" save={saveProgramStrategy} initialValue={strategy} fields={strategyFields}>
                <Stack direction="row" justifyContent="space-between" sx={{ m: 1, p: 1 }}>
                    <Typography variant="h6" >Lineup Selectors</Typography>
                    < IconButton onClick={addLineup} >
                        <AddIcon />
                    </IconButton>
                </Stack>
                {lineups && <EditingGrid gridItems={lineups} gridFieldColumns={gridColumns} deleteItem={removeLineup} saveItem={saveLineup} isLoading={lineupTypesLoading || lineupsLoading} />}
            </CustomForm>
        </Container>
    )
}

export default ProgramStrategyLayout;