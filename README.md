# BusMS

=> Main Table 

	+ User
		- id
		- fullname
		- phone
		- Usertype(admin, staff)
		- username
		- password
		- createddate
		- createdby
		- updateddate
		- updatedby
	
	+ bus
		- id
		- bus_type
		- bus_number
		- bus_plate_no
		- capacity_seat
		- created_date
		- created_by
		- updated_date
		- updated_by
	+ employee
		- id 
		- first_name
		- last_name
		- dob
		- gender
		- phone
		- email
		- address
		- Role
		- salary
		- created_date
		- created_by
		- updated_date
		- updated_by
	+ driver 
		- id
		- emp_id
		- bus_id
		- driver_name
		- driver_phone
		- created_date
		- created_by
		- updated_date
		- updated_by

	+ Customer
		- id
		- name
		- phone
		- email
		- address
		- created_date
		- created_by
		- updated_date
		- updated_by

	+ booking
		- id
		- schedule_id
		- customer_id 
		- number_of_seat
		- amount_per_seat
		- total_amount
		- date_booking
		- booking_status
		- created_date
		- created_by
		- updated_date
		- updated_by

	+ payment
		- id
		- amount_paid
		- payment_date
		- booking_id
		- created_date
		- created_by
		- updated_date
		- updated_by

	+ travel_schedule
		- id
		- bus_id
		- driver_id
		- starting_point
		- destination_to
		- schedule_date
		- start_time
		- arrival_time
		- amount_per_seat
		- description
		- created_date
		- created_by
		- updated_date
		- updated_by

