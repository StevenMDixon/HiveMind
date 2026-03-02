import { useParams } from "react-router-dom";
import { useState, Suspense, use, useEffect } from 'react'
import ErrorBoundary from '../../Components/ErrorBoundary';

import Container from '@mui/material/Container';
import Toolbar from '@mui/material/Toolbar';
import AppBar from '@mui/material/AppBar';
import Typography from '@mui/material/Typography';

//import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';

//import Card from '@mui/material/Card';
//import CardHeader from '@mui/material/CardHeader';

//import EditIcon from '@mui/icons-material/Edit';
//import SaveIcon from '@mui/icons-material/Save';
//import AddIcon from '@mui/icons-material/Add';

import Stack from '@mui/material/Stack';

import FormControl from '@mui/material/FormControl';
import InputLabel from '@mui/material/InputLabel';
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';

import Accordion from '@mui/material/Accordion';
import AccordionSummary from '@mui/material/AccordionSummary';
import AccordionDetails from '@mui/material/AccordionDetails';
import AccordionActions from '@mui/material/AccordionActions';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import IconButton from '@mui/material/IconButton';

import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';

interface Option {
    name: string,
    type: string,
    options: string
}
interface Filter {
    queryFilterID: number;
    field: string;
    operator: string;
    value: string;
}

interface Collection {
    collectionID: number;
    collectionName: string;
    filters: Filter[];
}

const fetchCollection = async (collectionId: number): Promise<Collection> => {
    const response = await fetch('/collections/' + collectionId);

    if (!response.ok) {
        throw new Error(`Failed to fetch media: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
};

const CollectionInfo = ({ collection }: { collection: Collection | undefined}) => {
    return (
        <Accordion defaultExpanded>
            <AccordionSummary
                expandIcon={<ExpandMoreIcon />}
                aria-controls="panel2-content"
                id="panel2-header"
            >
                <Typography component="span">Info</Typography>
            </AccordionSummary>
            <AccordionDetails>
                {
                    collection &&
                    <TextField id="outlined-basic" label="Collection Name" variant="outlined" defaultValue={collection.collectionName} />
                }
            </AccordionDetails>
        </Accordion>
    );
};

interface CollectionFiltersLayoutProps { filters: Filter[] | undefined, options: Option[], filterChange: (a: React.ChangeEvent<HTMLInputElement>) => void, addFilter: () => void, removeFilter: (a: number) => void }

const CollectionFiltersLayout = ({ filters, options, filterChange, addFilter, removeFilter }: CollectionFiltersLayoutProps) => {
    return (
        <Accordion defaultExpanded>
            <AccordionSummary
                expandIcon={<ExpandMoreIcon />}
                aria-controls="panel2-content"
                id="panel2-header"
            >
                <Typography component="span">Filters</Typography>
            </AccordionSummary>
            <AccordionDetails>
                {
                    filters && <CollectionFilter filters={filters} options={options} change={filterChange} remove={removeFilter} />
                }
            </AccordionDetails>
            <AccordionActions>
                <Button onClick={addFilter}>Add</Button>
            </AccordionActions>
        </Accordion>
    )
};

const getSelectableOptions = (field: string, options: Option[]): string[] => {
    let validOps: string[] = ["-"];
    const selectedOption = options.find(x => x.name == field);

    if (selectedOption) {
       validOps = validOps.concat(selectedOption.options.split(", "))
    }

    return validOps;
}

interface CollectionFilterProps { filters: Filter[], options: Option[], change: (a: React.ChangeEvent<HTMLInputElement>) => void, remove: (a: number)=> void }

const CollectionFilter = ({ filters, options, change, remove }: CollectionFilterProps) => {
    return (
        <Stack>
        {
                filters.map((x: Filter) => (
                    <Stack direction="row" key={x.queryFilterID}>
                <FormControl sx={{ minWidth: 200, mr: 1, mb: 1 }}>
                    <InputLabel>Field</InputLabel>
                            <Select

                        name={x.queryFilterID + " field"}
                        value={x.field}
                        onChange={change}
                           >
                        {
                            options.map(option => (
                                <MenuItem value={option.name}>{option.name}</MenuItem>
                            ))
                        }
                    </Select>
                </FormControl>
                <FormControl sx={{ minWidth: 200, mr: 1  }}>
                    <InputLabel>Operator</InputLabel>
                    <Select
                        name={x.queryFilterID + " operator"}
                        value={x.operator}
                        onChange={change}
                            >
                        {
                            getSelectableOptions(x.field, options).map(op => (<MenuItem value={op}>{op}</MenuItem>))
                        }
                    </Select>
                        </FormControl>
                        <FormControl>
                            <TextField label="Value" variant="outlined" value={x.value} />
                        </FormControl>
                        <FormControl>
                            <IconButton aria-label="delete">
                                <DeleteOutlineIcon color="error" onClick={() => remove(x.queryFilterID)} />
                            </IconButton>
                        </FormControl>
            </Stack>
        ))
        }
        </Stack>
        );
}

const dataPromise = fetch('/querysettings').then(res => res.json());

const CollectionLayout = ({ promise }: { promise: Promise<Collection>, save: (c: Collection) => Promise<void>}) => {
    const collection = use(promise);
    const queryOptions = use(dataPromise);

    const [collectionData, setCollectionData] = useState<Collection>();
    const [filterData, setFilterData] = useState<Filter[]>([]);

    const handleFilterChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { value, name } = event.target;
        const [id, input] = name.split(" ");

        if (filterData) {
            setFilterData(prevFilters =>
                prevFilters.map(filter => {
                    if (filter.queryFilterID === Number(id)) {
                        return { ...filter, [input]: value }
                    }

                    return filter;
                })
            );
        }
    }

    const removeFilter = (id: number) => {
        console.log(id)
        const newFilterData = filterData.filter(x => x.queryFilterID != id);
        setFilterData(newFilterData)
    }

    const addFilter = () => {
        const newFilterData = [...filterData, {queryFilterID: (filterData.length + 1) * -1, field: "", operator: "", value: "" } as Filter];
        setFilterData(newFilterData);
    }

    useEffect(() => {
        setCollectionData(collection);
        setFilterData(collection.filters);
    }, [collection])

    return (
        <Container sx={{ mt: 5 }}>
            <CollectionInfo collection={collectionData} />
            <CollectionFiltersLayout filters={filterData} options={queryOptions.options} filterChange={handleFilterChange} addFilter={addFilter} removeFilter={removeFilter} />
        </Container>
    )
}


const CollectionDetail = () => {
    const { id } = useParams();

    const [collectionPromise, setCollectionPromise] = useState(() => fetchCollection(Number(id)));

    const saveCollection = async (collection: Collection): Promise<void> => {
        await fetch('/collections/'+ id, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(collection),
        });
        setCollectionPromise(() => fetchCollection(Number(id)))
    }

    return (
        <Container disableGutters maxWidth={false}>
            <AppBar position="static" color="secondary">
                <Toolbar>
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                        Collection Details
                    </Typography>
                    <Button color="inherit">Save</Button>
                </Toolbar>
            </AppBar>
            <ErrorBoundary
                fallback={(error) => (
                    <p>{error.message}</p>
                )}>
                <Suspense fallback={<p> Loading </p>}>
                    <CollectionLayout promise={collectionPromise} save={saveCollection} />        
                </Suspense>
            </ErrorBoundary>
        </Container>
    )
}

export default CollectionDetail;