import React, {ChangeEvent, useState} from 'react';
import {Button, TextField, Box, Stack} from '@mui/material';

interface SearchFormProps {
    searchKeyword: string;
    websiteUrl: string;
    onSubmit: (searchKeyword: string, websiteUrl: string) => void;
    onSearchKeywordChange: (value: string) => void;
    onWebsiteUrlChange: (value: string) => void;
}

export default function SearchForm({
                                       searchKeyword,
                                       websiteUrl,
                                       onSubmit,
                                       onSearchKeywordChange,
                                       onWebsiteUrlChange
                                   }: SearchFormProps) {
    const handleSearch = (e: React.FormEvent) => {
        e.preventDefault();
        onSubmit(searchKeyword, websiteUrl);
    };

    const handleKeywordChange = (e: ChangeEvent<HTMLInputElement>) => {
        onSearchKeywordChange(e.target.value);
    };

    const handleUrlChange = (e: ChangeEvent<HTMLInputElement>) => {
        onWebsiteUrlChange(e.target.value);
    };

    return (
        <Box component="form" onSubmit={handleSearch} noValidate autoComplete="off"
             sx={{
                 display: 'flex',
                 justifyContent: "center",
                 gap: 2
             }}>
            <TextField
                fullWidth
                label="Search Keyword"
                value={searchKeyword}
                onChange={handleKeywordChange}
                margin="normal"
            />
            <TextField
                fullWidth
                label="Website URL"
                value={websiteUrl}
                onChange={handleUrlChange}
                margin="normal"
            />
            <Button type="submit" variant="contained" color="primary"
                    sx={{
                        alignSelf: 'center', // Align button to the top of the container
                        marginTop: 1,
                        paddingY: 1,
                        paddingX: 4
                    }}>
                Search
            </Button>
        </Box>
    );
};