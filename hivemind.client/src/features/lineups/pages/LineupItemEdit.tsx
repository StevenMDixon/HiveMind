import { useState } from 'react'

import type { QueryLineupItem, LineupItem } from '../types';
import type { Query } from '../../queries/types';

import CustomForm from '@/components/CustomForm';
import { type CustomFormField } from '@/components/FormFields';
import { type EditingGridColumns } from '@/components/EditingGrid';
import EditingGrid from '@/components/EditingGrid';

import { Container, AppBar, Toolbar, Button } from "@mui/material";
import { Box, Stack, Typography, IconButton,  } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';

import { useParams, useNavigate } from "react-router-dom";

import { toEnumOptions, getValueFromMappedObject } from "@/utilities/FormOptionsMapper"

import { useQuery, useQueries } from '@tanstack/react-query';
import { lineupItemQueryOptions, padtoQueryOptions, queryTypesQueryOptions, playoutTypesQueryOptions } from '../api/queryOptions';
import { queriesQueryOptions } from '../../queries/api/queryOptions'

import { useLineupItemUpdateHook } from '../hooks/lineupItemHooks';
interface ItemEditorProps {
    lineupItemData: LineupItem;
}

const ItemEditor = ({ lineupItemData }: ItemEditorProps) => {
    const { padTo, queryTypes, playoutTypes, availableQueries, isLoading } = useQueries({
        queries: [
            padtoQueryOptions(),
            queryTypesQueryOptions(),
            playoutTypesQueryOptions(),
            queriesQueryOptions(),
        ] as const,
        combine: ([padTo, queryTypes, playoutTypes, queries]) => ({
            padTo: padTo.data ?? {},
            queryTypes: queryTypes.data ?? {},
            playoutTypes: playoutTypes.data ?? {},
            availableQueries: queries.data ? toEnumOptions(queries.data as Query[], "queryId", "queryName", true) : {},
            isLoading:
                padTo.isLoading ||
                queryTypes.isLoading ||
                playoutTypes.isLoading ||
                queries.isLoading,
        })
    })

    const disableField = (field: string, item: QueryLineupItem) => {
        switch (field) {
            case 'padTo': return item.queryType != 4 ;
            case 'playDuration': return item.queryType != 4; 
            case 'playCount': return item.queryType != 4; 
            case 'playoutType': return item.queryType != 4; 
            case 'index': return item.queryType != 4; 
            default: return false
        }
    }

    const columns = [
        { name: 'queryId', display: 'Query', initialValue: 0, type: 'Select', format: (e: number) => getValueFromMappedObject(availableQueries, e), options: availableQueries },
        { name: 'queryType', display: 'Assigned Type', initialValue: 0, type: 'Select', format: (e: number) => getValueFromMappedObject(queryTypes, e), options: queryTypes },
        { name: 'playoutType', display: 'Playout Type', initialValue: 0, type: 'Select', format: (e: number) => getValueFromMappedObject(playoutTypes, e), options: playoutTypes, disabled: disableField },
        { name: 'playDuration', display: 'Duration', initialValue: 0, type: 'Number', validator: (n: number) => n > -1, disabled: disableField },
        { name: 'playCount', display: 'Count', initialValue: 0, type: 'Number', validator: (n: number) => n > -1, disabled: disableField },
        { name: 'padTo', display: 'Pad To', initialValue: 0, type: 'Select', format: (e: number) => getValueFromMappedObject(padTo, e), options: padTo, disabled: disableField },
        { name: 'index', display: 'Index', initialValue: 0, type: 'Number', disabled: disableField },
        { name: 'group', display: 'Group', initialValue: 0, type: 'Number'},

    ] as EditingGridColumns<QueryLineupItem>[]

    const [lineupQueryItems, setLineupQueryItems] = useState(lineupItemData.queries);
    const [lineupType, setLineupType] = useState(lineupItemData.type);

    const lineupItemFields = [
        { name: 'name', display: "Name", type: "Text", initialValue: lineupItemData.name },
        { name: 'type', display: "Lineup Item Type", type: "Radio", initialValue: lineupItemData.type, options: ["Generic", "Block"]
    }
    ] as CustomFormField[];

    const addItem = async () => {
        const currentIndex = lineupQueryItems.length + 1;
        const newItem = { queryLineupItemId: currentIndex * -1, lineupItemId: lineupItemData.lineupItemId, queryId: 0, queryType: 0, playoutType: 0, index: 0, playDuration: 0, padTo: 0, playCount: 0, group: 0 } as QueryLineupItem
        setLineupQueryItems([...lineupQueryItems, newItem]);
    }

    const removeItem = (lineupQueryItem: QueryLineupItem) => {
        setLineupQueryItems(lineupQueryItems.filter(x => x.queryLineupItemId != lineupQueryItem.queryLineupItemId));
    }

    const handleSaveLineupQueryItem = (lineupQueryItem: QueryLineupItem) => {
        const itemIdx = lineupQueryItems.findIndex(item => item.queryLineupItemId == lineupQueryItem.queryLineupItemId)
        const updated = [...lineupQueryItems]
        updated[itemIdx] = lineupQueryItem

        setLineupQueryItems(updated);
    }

    const { mutate: updateLineupItem } = useLineupItemUpdateHook();

    const handleUpdateLineupItem = async (editedLineupItem: LineupItem) => {
        const updatedLineupItem = { ...editedLineupItem, queries: lineupQueryItems }
        updateLineupItem(updatedLineupItem);
    }

    const setLineupItemType = (input: string) => setLineupType(input);

    return (
        <Container sx={{ mt: "1.5em", p: "1em" }}>
            <CustomForm title="Editing Lineup Item" save={handleUpdateLineupItem} initialValue={lineupItemData} fields={lineupItemFields} watchFields={{
                type: (value) => {
                    // value is inferred as correct type of "type"
                    setLineupItemType(value);
                }
            }}>
                {lineupType == 'Generic' ?
                <Box>
                    <Stack direction="row" justifyContent="space-between" sx={{ m: 1, p: 1 }}>
                        <Typography variant="h6" >Generic Item Slots:</Typography>
                        < IconButton onClick={addItem} >
                            <AddIcon />
                        </IconButton>
                    </Stack>
                    <EditingGrid gridItems={lineupQueryItems} gridFieldColumns={columns} deleteItem={removeItem} saveItem={handleSaveLineupQueryItem} isLoading={isLoading} />
                </Box>
                    : <p> sheeesh</p>    }
            </CustomForm>
        </Container>
    );
}


const LineupItemEdit = () => {

    const { itemId } = useParams();

    const { data: lineupItem } = useQuery(lineupItemQueryOptions(Number(itemId ?? '0')));

    const navigate = useNavigate();

    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Editing Lineup Item: {itemId}
                    </Typography>
                    <Button onClick={() => navigate(-1) }>
                        Back
                    </Button>
                </Toolbar>
            </AppBar>
            {lineupItem && <ItemEditor lineupItemData={lineupItem} />}
        </Container>
    )
}

export default LineupItemEdit