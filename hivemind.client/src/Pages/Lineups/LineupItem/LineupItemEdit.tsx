import { ApiClient } from '../../../Api/ApiClient';

import { useState, Suspense, use, } from 'react'

import type { QueryLineupItem, LineupItem } from '../../../Types/Lineup';
import type { EnumOptionsObj } from '../../../Types/General'; 
import type { Query } from '../../../Types/Query';

import ErrorBoundary from "../../../Components/ErrorBoundary";

import CustomForm from '../../Components/CustomForm';
import { type CustomFormField } from '../../Components/FormFields';

import { Container, AppBar, Toolbar, Button } from "@mui/material";
import { Box, Stack, Typography, IconButton,  } from "@mui/material";

import { useParams, useNavigate } from "react-router-dom";

import { type EditingGridColumns } from '../../Components/EditingGrid';
import EditingGrid from '../../Components/EditingGrid';

import AddIcon from '@mui/icons-material/Add';

import { useGlobalNotification } from '../../../Dashboard/useGlobalNotification';

import { toEnumOptions, getValueFromMappedObject } from "../../../Utilities/FormOptionsMapper"

interface ItemEditorProps {
    lineupItemPromise: Promise<LineupItem>;
    queryTypePromise: Promise<EnumOptionsObj>;
    playoutTypePromise: Promise<EnumOptionsObj>;
    availableQueryPromise: Promise<Query[]>
    padToPromise: Promise<EnumOptionsObj>;
}

const ItemEditor = ({ lineupItemPromise, queryTypePromise, playoutTypePromise, availableQueryPromise, padToPromise }: ItemEditorProps) => {
    const queryTypes = use(queryTypePromise);
    const queries = use(availableQueryPromise);
    const playOutTypes = use(playoutTypePromise);
    const lineupItemData = use(lineupItemPromise);
    const padToData = use(padToPromise); 

    const { showNotification } = useGlobalNotification();

    const getQueryName = (id: number) => {
        const foundQuery = queries.find(x => x.queryId == id);
        return foundQuery?.queryName ?? 'none';
    }

    const [lineupItem, setLineupItem] = useState(lineupItemData);
    const [lineupQueryItems, setLineupQueryItems] = useState(lineupItemData.queries);

    const lineupItemFields = [
        { name: 'name', display: "Name", type: "Text", initialValue: lineupItem.name },
        { name: 'type', display: "Lineup Item Type", type: "Radio", initialValue: lineupItem.type, options: ["Generic", "Block"] }
    ] as CustomFormField[];

    const columns = [
        { name: 'queryId', display: 'Query', initialValue: 0, type: 'Select', format: (e: number) => getQueryName(e), options: toEnumOptions(queries, "queryId", "queryName", true)},
        { name: 'queryType', display: 'Assigned Type', initialValue: 0, type: 'Select', format: (e: number) => getValueFromMappedObject(queryTypes, e), options: queryTypes },
        { name: 'playoutType', display: 'Playout Type', initialValue: 0, type: 'Select', format: (e: number) => getValueFromMappedObject(playOutTypes, e), options: playOutTypes },
        { name: 'playDuration', display: 'Duration', initialValue: 0, type: 'Number', validator: (n: number) => n > -1 },
        { name: 'playCount', display: 'Count', initialValue: 0, type: 'Number', validator: (n: number) => n > -1 },
        { name: 'padTo', display: 'Pad To', initialValue: 0, type: 'Select', format: (e: number) => getValueFromMappedObject(padToData, e), options: padToData }
    ] as EditingGridColumns<QueryLineupItem>[]

    const addItem = async () => {
        const currentIndex = lineupQueryItems.length + 1;
        const newItem = { queryLineupItemId: currentIndex * -1, lineupItemId: lineupItem.lineupItemId, queryId: 0, queryType: 0, playoutType: 0, index: currentIndex, playDuration: 0, padTo: 0, playCount: 0 } as QueryLineupItem
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

    const handleUpdateLineupItem = async (editedLineupItem: LineupItem) => {
        const updatedLineupItem = { ...editedLineupItem, queries: lineupQueryItems }
        const response = await ApiClient.updateLineupItem(updatedLineupItem);

        if (response.ok) {
            setLineupItem(updatedLineupItem);
            showNotification("Lineup Item Saved Successfully", "success")
        } else {
            showNotification("Error Saving Lineup Item", "error")
        }
    }
    return (
        <Container sx={{ mt: "1.5em", p: "1em" }}>
            <CustomForm title="Editing Lineup Item" save={handleUpdateLineupItem} initialValue={lineupItem} fields={lineupItemFields}>
                <Box>
                    <Stack direction="row" justifyContent="space-between" sx={{ m: 1, p: 1 }}>
                        <Typography variant="h6" >Lineup Item Slots:</Typography>
                        < IconButton onClick={addItem} >
                            <AddIcon />
                        </IconButton>
                    </Stack>
                    <EditingGrid gridItems={lineupQueryItems} gridFieldColumns={columns} deleteItem={removeItem} saveItem={handleSaveLineupQueryItem} />
                </Box>
            </CustomForm>
        </Container>
    );
}


const LineupItemEdit = () => {

    const { itemId } = useParams();
    const [availableQueries] = useState(() => ApiClient.fetchQueries());
    const [availablePlayoutTypes] = useState(() => ApiClient.fetchPlayoutTypes());
    const [availableQueryTypes] = useState(() => ApiClient.fetchQueryTypes());
    const [availablePadTo] = useState(() => ApiClient.fetchPadToOptions());

    const correctedId = itemId ?? ""

    const [lineupItem] = useState(() => ApiClient.fetchLineupItem(Number(correctedId)));

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
            <ErrorBoundary
                fallback={(error) => (
                    <p>{error.message}</p>
                )}>
               <Suspense>
                    <ItemEditor lineupItemPromise={lineupItem} availableQueryPromise={availableQueries} playoutTypePromise={availablePlayoutTypes} queryTypePromise={availableQueryTypes} padToPromise={availablePadTo} />
                </Suspense>
            </ErrorBoundary>
        </Container>
    )
}

export default LineupItemEdit