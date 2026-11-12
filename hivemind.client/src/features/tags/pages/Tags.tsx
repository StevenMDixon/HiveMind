import type { Tag } from '../types';
import { useState } from 'react';
import { useQuery } from '@tanstack/react-query';
import { tagQuerySearchOptions, categoryQueryOptions } from '../api/queryOptions';

import EditingGrid from '@/components/EditingGrid';
import { type EditingGridColumns } from '@/components/EditingGrid';

import Container from '@mui/material/Container';
import Header from '@/components/Header';
import SearchIcon from '@mui/icons-material/Search';

import type { PagingData } from '../../../components/CustomTable';

import { useTagDeleteHook, useTagUpdateHook, useTagCreateHook } from '../hooks/tagHooks';
import { TextField, InputAdornment, Pagination } from '@mui/material';

import TagCreateDialog from '../components/TagCreateDialog';

const Tags = () => {
    const [search, setSearch] = useState('');
    const [pageIndex, setPageIndex] = useState(1);
    const [pageSize] = useState(20);

    const { mutate: saveTag } = useTagUpdateHook();
    const { mutate: deleteTag } = useTagDeleteHook();
    const { mutate: createTag } = useTagCreateHook();

    const { data: categories } = useQuery(categoryQueryOptions());
    const { data: tags, isLoading } = useQuery(tagQuerySearchOptions(search, pageIndex, pageSize));

    const formatCategory = (id: number) => categories ? categories[id] : id;
    const columns = [
        { name: 'id', display: 'Id', type: 'Text', disabled: true },
        { name: 'name', display: 'Tag', initialValue: '', type: 'Text', disabled: true},
        { name: 'category', display: 'Category', initialValue: '', type: 'Select', options: categories, format: formatCategory},
    ] as EditingGridColumns<Tag>[]


    const handleUpdate = (tag: Tag) => {
        saveTag(tag);
    };

    const handleDelete = (tag: Tag) => {
        deleteTag(tag);
    };

    const Search = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setSearch(e.target.value);
    }
    
    const HandlePageIndexChange = (_: React.ChangeEvent<unknown>, value: number) => {
        console.log(value);
        setPageIndex(value);
    }

    const HandleCreateTag = (tag: Tag) => {
        createTag(tag)
    }

    const paging = {
        currentPage: pageIndex,
        dataTotal: tags?.dataCount,
        pageSize: pageSize,
        changePage: HandlePageIndexChange
     } as PagingData;   

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title="Tags">
                {categories && <TagCreateDialog createTag={HandleCreateTag} categories={categories} />}
            </Header>
             
            <Container sx={{ mt: "1em" }} >
                <Container sx={{ display: 'flex', justifyContent:'flex-end', m: '1em', marginRight: '0em'}}>
                    <TextField 
                        sx ={{minWidth: '400px'}}
                        size="small"
                        type="search"
                        variant="outlined" 
                        onChange={Search}
                        slotProps={{
                            input: {
                                startAdornment: <InputAdornment position="start"><SearchIcon /></InputAdornment>,
                            },
                        }}
                        />
                    
                </Container>
                <EditingGrid gridItems={tags?.data} gridFieldColumns={columns} saveItem={handleUpdate} deleteItem={handleDelete} isLoading={isLoading} />
                <Container  sx={{ display: 'flex', width: '100%', justifyContent:'center', mt: '1em'}}>
                    {paging?.dataTotal && <Pagination count={Math.ceil(paging.dataTotal/ paging.pageSize)} page={paging.currentPage} onChange={paging.changePage}/>}
                </Container>
            </Container>
        </Container>
    )
}

export default Tags;