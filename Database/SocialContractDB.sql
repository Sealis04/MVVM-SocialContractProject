-- phpMyAdmin SQL Dump
-- version 5.1.4-dev
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 28, 2022 at 03:17 AM
-- Server version: 10.4.24-MariaDB
-- PHP Version: 8.1.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `socialcontractdb`
--

-- --------------------------------------------------------

--
-- Table structure for table `tbl_adminacc`
--

CREATE TABLE `tbl_adminacc` (
  `admin_ID` int(11) NOT NULL,
  `admin_user` varchar(255) NOT NULL,
  `admin_pass` varchar(255) NOT NULL,
  `admin_salt` varchar(255) NOT NULL,
  `admin_type` tinyint(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tbl_adminacc`
--

INSERT INTO `tbl_adminacc` (`admin_ID`, `admin_user`, `admin_pass`, `admin_salt`, `admin_type`) VALUES
(1, 'admin', 'FlHOTGLN6OPm8DOaiM3TSxLNHRXRSk4t', 'wxkEWR5I7w8hkg==', 0);

-- --------------------------------------------------------

--
-- Table structure for table `tbl_events`
--

CREATE TABLE `tbl_events` (
  `event_ID` int(11) NOT NULL,
  `event_name` varchar(255) NOT NULL,
  `event_date` date NOT NULL,
  `event_supervisor` varchar(255) NOT NULL,
  `event_PDF` varchar(255) NOT NULL,
  `event_venue` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `tbl_recordtbl`
--

CREATE TABLE `tbl_recordtbl` (
  `record_ID` int(11) NOT NULL,
  `record_s_ID` varchar(255) NOT NULL,
  `record_SchoolYear` int(11) NOT NULL,
  `record_FirstSemester` int(11) NOT NULL DEFAULT 0,
  `record_SecondSemester` int(11) NOT NULL DEFAULT 0,
  `record_Summer` int(11) NOT NULL DEFAULT 0,
  `record_SocialContract` varchar(255) NOT NULL,
  `record_IsRemoved` tinyint(4) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `tbl_studentinfo`
--

CREATE TABLE `tbl_studentinfo` (
  `tbl_ID` int(11) NOT NULL,
  `s_ID` varchar(11) NOT NULL,
  `s_fn` varchar(255) NOT NULL,
  `s_mn` varchar(255) NOT NULL,
  `s_ln` varchar(255) NOT NULL,
  `s_batchNo` int(11) NOT NULL,
  `s_Course` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tbl_adminacc`
--
ALTER TABLE `tbl_adminacc`
  ADD PRIMARY KEY (`admin_ID`);

--
-- Indexes for table `tbl_recordtbl`
--
ALTER TABLE `tbl_recordtbl`
  ADD PRIMARY KEY (`record_ID`);

--
-- Indexes for table `tbl_studentinfo`
--
ALTER TABLE `tbl_studentinfo`
  ADD PRIMARY KEY (`tbl_ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tbl_adminacc`
--
ALTER TABLE `tbl_adminacc`
  MODIFY `admin_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `tbl_recordtbl`
--
ALTER TABLE `tbl_recordtbl`
  MODIFY `record_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=36;

--
-- AUTO_INCREMENT for table `tbl_studentinfo`
--
ALTER TABLE `tbl_studentinfo`
  MODIFY `tbl_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
