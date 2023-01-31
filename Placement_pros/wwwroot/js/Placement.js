
$("#country").change(function () {
		debugger;
		$("#state").empty();
		var id = $("#country").val();
		$.ajax({
			type: 'GET',
			url: '/Account/LoadState',
			data: { id: id },
			success: function (states) {
				debugger;
				$.each(states, function (i, state) {
					$("#state").append('<option value="' + state.id + ' ">' + state.name + '</option>');
				});
			},
			Error: function (ex) {
				alert('Failed to retreive state .' + ex);
			}
		});

	})



function myEducation() {
	debugger;
	var data = {};
	data.Name = $("#name").val();
	data.Grade = $("#grade").val();
	data.Degree = $("#degree").val();
	data.FieldOfStudy = $("#fieldofstudy").val();
	data.StartDate = $("#startdate").val();
	data.EndDate = $("#enddate").val();
	if (data != "") {
		var educationQalification = data;
		$.ajax({
			type: 'Post',
			dataType: 'Json',
			url: '/user/EducationalQualification',
			data:
			{
				education:educationQalification,
			},
			success: function (result) {
				debugger;
				if (!result.isError) {
					var url = '/User/Profile';
					successAlertWithRedirect(result.msg, url);
				}
				else {
					errorAlert(result.msg);
				}
			},
			error: function (ex) {
				errorAlert("Error Occured,try again.");
			}
		})
	}
}
var profilePicture = "";

function ConvertPictureToBase64(e) {
	debugger;
	var profileInputId = e.target.id;
	var pix = document.getElementById(profileInputId).files;
	if (pix[0] != null) {
		const reader = new FileReader();
		reader.readAsDataURL(pix[0]);
		reader.onload = function () {
			profilePicture = reader.result;
		}
	}
}

function myPersonalinfo() {
	debugger;
	var data = {};
	data.FirstName = $("#firstname").val();
	data.LastName = $("#lastname").val();
	data.Email = $("#email").val();
	data.PhoneNumber = $("#Phone").val();
	data.Country = $("#country").val();
	data.State = $("#state").val();
	data.Address = $("#address").val();
	data.Gender = $("#gender").val();
	data.Birthdate = $("#birthdate").val();
	data.Description = $("#describe").val();
	data.ProfilePicture = profilePicture;
	if (data != "") {
		var editPersonalInfo = data;
		$.ajax({
			type: 'Post',
			dataType: 'Json',
			url: '/user/EditPersonalInfo',
			data:
			{
				personalInfo: editPersonalInfo,
			},
			success: function (result) {
				debugger;
				if (!result.isError) {
				var url = '/User/Profile';
				//location.href = url;
					successAlertWithRedirect(result.msg, url);
				}
				else {
					errorAlert(result.msg);
				}
			},
			error: function (ex) {
				errorAlert("Error Occured,try again.");
			}
		})
	}
}


$(document).ready(function () {
	$(".summernote").summernote({
		height: 100,
		tabsize: 2
	});
});
var Picture = "";

//function ConvertPictureToBase64(e) {
//	var profileInputId = e.target.id;
//	var picture = document.getElementById(profileInputId).files;
//	if (picture[0] != null) {
//		const reader = new FileReader();
//		reader.readAsDataURL(picture[0]);
//		reader.onload = function () {
//			picture = reader.result;
//		}
//	}
//}
function myDelistjob(id) {
	debugger;
	$.ajax({
		type: 'Post',
		dataType: 'Json',
		url: '/Admin/Delist',
		data:
		{
			id: id,
		},
		success: function (result) {
			debugger;
			if (!result.isError) {
				//ShowSwal("success", result.msg);
				successAlert(result.msg)
			}
		},
		error: function (ex) {
			errorAlert("Error Occured,try again.");
		}
			
	})
}

function redirect() {
	debugger;
	location.href = '/User/JobApplication';
}

var base64CV = "";
var base64Resume = "";

function ConvertFilesToBase64(e) {
	debugger;
	var fileInputId = e.target.id;
	var fileName = e.target.name;
	var file = document.getElementById(fileInputId).files;
	if (file[0] != null) {
		const reader = new FileReader();
		reader.readAsDataURL(file[0]);
		reader.onload = function () {
			if (fileName == "Resume") {
				base64Resume = reader.result;
			} else {
				base64CV = reader.result;
            }
		 
		}
	}
}


function myJob(jobId) {
	debugger;
	var data = {};
	data.Resume = base64Resume;
	data.Cv = base64CV;
	data.JobId = jobId;
	if (data.Cv != "" && data.Resume != "" && data.JobId != "") {
		let jobData = JSON.stringify(data);
		$.ajax({
			type: 'Post',
			dataType: 'Json',
			url: '/User/JobApplication',
			data:
			{
				jobApplication: jobData,
			},
			success: function (result) {
				debugger;
				if (!result.isError) {
					var url = '/User/Jobs';
					successAlertWithRedirect(result.msg, url);
				}
			},
			error: function (ex) {
				errorAlert("Error Occured,try again.");
			}

		})
	} else {
		errorAlert("Error Occured,try again.");
	}
}
function myfilter() {
		debugger;
		var data = {};
		data.CompanyName = $("#companyname").val();
		data.Location = $("#location").val();
		data.Jobtitle = $("#jobtitle").val();
	if (data.CompanyName != "" || data.Location != "" || data.Jobtitle != "") {
		var filter = JSON.stringify(data);
			$.ajax({
				type: 'Post',
				dataType: 'Json',
				url: '/User/JobSearch',
				data:
				{
					searchData: filter,
				},
				success: function (result) {
					debugger;
					if (!result.isError) {
						$("#addJob").empty();
						$.each(result.data, function (i, job) {
							$("#addJob").append(
								' <div class="card border hover p-4 mt-2 border-info mb-0 text-black" id="' + job.id + '">' +
								'<div class="row" ><div class="col-8  bbb-text "><div class=" row list-inline-item">' +
								'<h3 class="fs-14 mb-1 ml-3 " style="font-size:16px">' + job.title + '</h3></div>' +
								'<div class=" row list-inline-item"><h5 class=" fs-14 mb-1 text-info ml-3">' + job.companyName + '</h5></div>' +
								'<div class=" row list-inline-item"><p class=" fs-14 mb-1 ml-3"><i class="fa fa-map-marker"></i>' + job.location + '</p> </div>' +
								'<div class=" row list-inline-item stretched-link"><p class=" fs-14 mb-1  ml-3"> $' + job.salary + '</div>' +
								'<div class="stretched-link row ml-2"><span class="badge bg-info p-1">' + job.type + '</span> </div></div>' +
								'<div class="col-md-3"><a class="d-flex justify-content-end" style="color:#FFFF33;height:20px; margin-left:10px;">' +
								'<img src="/image/avatar1.jpg" height="100" width="100" class="rounded-circle" alt="Cinque Terre"/></a></div><div>' +
								'<a class="btn btn-info float-left offset-4" asp-controller="User" asp-action="JobApplication" asp-route-id="'+ job.id +'">Apply</a></div></div></div>'
							);
                        });
					}
					else {
						errorAlert(result.msg);
					}

				},
				error: function (ex) {
					errorAlert("Error Occured,try again.");
				}

			})
		} else {
			errorAlert("Fill the input ti filter.");
		}

    }
