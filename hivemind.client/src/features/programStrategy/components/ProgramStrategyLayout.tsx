import { IconButton, Stack, Box } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';
import Typography from '@mui/material/Typography';

import CustomForm from "@/components/CustomForm";
import { type CustomFormField } from '@/components/FormFields';
import { type ProgramStrategy, type ProgramStrategyLineup, type ProgramStrategyEvent } from '../types';

import { useProgramStrategyUpdateHook } from '../hooks/programStrategyHooks';

import { useState } from 'react'
import type { EnumOptionsObj } from '@/utilities/types';

import EditingGrid, { type EditingGridColumns } from '@/components/EditingGrid';
import { lineupSelectionTypesQueryOptions, lineupsQueryOptions, queriesQueryOptions} from '../api/queryOptions'
import { useQuery } from '@tanstack/react-query';


interface ProgramStrategyLayoutProps {
    strategy: ProgramStrategy;
}

const ProgramStrategyLayout = ({ strategy }: ProgramStrategyLayoutProps) => {

    const { mutate: saveStrategy } = useProgramStrategyUpdateHook();

    const { data: selectionTypes, isLoading: lineupTypesLoading } = useQuery(lineupSelectionTypesQueryOptions());
    const { data: availableLineups, isLoading: lineupsLoading } = useQuery(lineupsQueryOptions());
    const { data: availableQueries, isLoading: queriesLoading } = useQuery(queriesQueryOptions());

    const [lineups, setLineupsData] = useState<ProgramStrategyLineup[]>(strategy.programStrategyItems ?? []);
    const [events, setEventsData] = useState<ProgramStrategyEvent[]>(strategy.programStrategyEvents ?? []);
    
    const mappedLineups = availableLineups?.reduce((acc: EnumOptionsObj, cur) => { acc[cur.id] = cur.name; return acc; }, {});

    const formatLineup = (item: number) => {
        return mappedLineups ? mappedLineups[item] : item;
    }

    const formatTypes = (item: number) => {
        return selectionTypes ? selectionTypes[item] : item;
    }

    const formatQueries = (item: number) => {
        return availableQueries ? availableQueries[item] : item;
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
            id: e.id,
            name: e.name,
            startDate: e.startDate,
            endDate: e.endDate,
            active: e.active,
            advancedDays: e.advancedDays,
            programStrategyItems: lineups,
            programStrategyEvents: events
        } as ProgramStrategy


        saveStrategy(newData);
    }

    const removeLineup = (item: ProgramStrategyLineup) => {
        const newFilterData = lineups.filter(x => x.id != item.id)
        setLineupsData(newFilterData)
    }

    const addLineup = () => {
        const newFilterData = [...lineups, { id: (lineups.length + 1) * -1, lineUpId: 0, selectionType: 0, selectionOption: "" } as ProgramStrategyLineup];
        setLineupsData(newFilterData);
    }

    const saveLineup = (item: ProgramStrategyLineup) => {
        setLineupsData(lineups.map(lineup => lineup.id == item.id ? item : lineup));
    }

    const addEvent = () => {
        const newEventData = [...events, { id: (events.length + 1) * -1, programStrategyId: strategy.id, name: "" } as ProgramStrategyEvent];
        setEventsData(newEventData);
    } 
    const removeEvent = (item: ProgramStrategyEvent) => {
        const newEventData = events.filter(x => x.id != item.id)
        setEventsData(newEventData)
    }

    const saveEvent = (item: ProgramStrategyEvent) => {
        setEventsData(events.map(x => x.id == item.id ? item : x));
    }

    const lineupGridColumns = [
        { name: 'lineUpId', display: 'Line Up', initialValue: 0, type: 'Select', format: formatLineup, options: mappedLineups },
        { name: 'selectionType', display: 'Type', initialValue: 0, type: 'Select', format: formatTypes, options: selectionTypes },
        { name: 'selectionOption', display: 'Option', initialValue: 0, type: 'Text' }
    ] as EditingGridColumns<ProgramStrategyLineup>[]

    const eventGridColumns = [
        { name: 'name', display: 'Name', initialValue: '', type: 'Text' },
        { name: 'promoDays', display: 'Promo Days', initialValue: 0, type: 'Number' },
        { name: 'eventDate', display: 'Date', initialValue: '1/1/1900', type: 'Text' },
        { name: 'queryId', display: 'Query', initialValue: '0', type: 'Select', options: availableQueries, format: formatQueries }
    ] as EditingGridColumns<ProgramStrategyEvent>[]

    return (
        <Box sx={{ mt: 5 }}>
            <Stack direction="row" sx={{ justifyContent: 'space-between' }} >
                <Box sx={{ width: '30%' }}>
                    <CustomForm title="Editing Program Strategy" save={saveProgramStrategy} initialValue={strategy} fields={strategyFields}/>
                </Box>
                <Stack sx={{ width: '65%' }}>
                    <Box>
                        <Stack direction="row" justifyContent="space-between" sx={{ m: 1, p: 1 }}>
                                <Typography variant="h6" >Playout Strategy</Typography>
                                < IconButton onClick={addLineup} >
                                    <AddIcon />
                                </IconButton>
                        </Stack>
                            {lineups && <EditingGrid gridItems={lineups} gridFieldColumns={lineupGridColumns} deleteItem={removeLineup} saveItem={saveLineup} isLoading={lineupTypesLoading || lineupsLoading} />}
                    </Box>
                    <Box>
                        <Stack direction="row" justifyContent="space-between" sx={{ m: 1, p: 1 }}>
                                <Typography variant="h6" >Event Promos</Typography>
                                < IconButton onClick={addEvent} >
                                    <AddIcon />
                                </IconButton>
                        </Stack>
                        {events && <EditingGrid gridItems={events} gridFieldColumns={eventGridColumns} deleteItem={removeEvent} saveItem={saveEvent} isLoading={queriesLoading} />}
                    </Box>
                    </Stack>
            </Stack>
            
        </Box>
    )
}

export default ProgramStrategyLayout;