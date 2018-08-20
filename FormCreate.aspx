<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FormCreate.aspx.cs" Inherits="FormCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <style>
        .contain {
            width: 80%;
            margin: auto;
            margin-top: 5%;
        }

        .btn {
            margin-right: 2%;
            margin-top: 2%;
        }

        #inner {
            width: 45%;
        }

        #newContent {
            width: 45%;
        }

        .form-control {
            width: 45%;
        }

    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

 
    <div class="contain">
        <form action="#" method="post" runat="server" onsubmit="return saveValidate()">
        <h3>New Form Creation -- <a href="Homepage.aspx">Return to Homepage</a></h3>
        <br />
        <div id="inner">
            <h4>What is your new form's title? :</h4>
            <input type="text" id="formtitle" name="formtitle" placeholder="Enter a form title." class="form-control" required />
            <label id="errLbl"></label>
            <div id="create"></div><hr />
            <input type="hidden" id="totalQ" name="totalQ"/>
            <div class="btn btn-primary" id="addBtn">Add a New Question</div>
            <div class="btn btn-primary" id="addText">Add Text</div>

            <br />
            <input type="submit" id="saveSubmit" class="btn btn-success" text="Save and Finish Form Creation" />
            <!--
            <div class="btn btn-success" id="saveBtn">Save Form</div>
            <asp:Button ID="saveSubmit" runat="server" Text="Save Form" CssClass="btn btn-primary"/>-->

            </div>
            <script>

                function saveValidate(event) {
                    let kosher = true;
                    for (let x in $("div#contain").children("input[type='text']")) {
                    if ($("#totalQ").val() == 0) {
                        $("#errLbl").html("You need to add at least one question!");
                        kosher = false;

                    }
                        /*
                    else if (x.val().length() == 0) {
                            let text = '<br><label id="' + $(this).toString() + '">You need to fill out this required field</label><br />';
                            x.after()
                            kosher = false;
                    }*/ 
                    else if (x.val().length() != 0) {
                        $(this.toString()).remove();
                    }
                    

                    }
                    if (kosher == false) {
                        return false;
                    }
                    return true;
                }
                let qnum = 0;
                let ddlMem;
                $("#totalQ").val(qnum);


                /*
                $("#saveBtn").click(function () {
                    if ($("#formtitle").val() == "") {
                        $("#errLbl").html("You need to enter a form title!");
                    }
                   
                    else {
                        $("#saveSubmit").trigger("click");
                    }


                });*/

                $("#addText").click(function () {
                    qnum = parseInt($("#totalQ").val()) + 1;
                    let tDiv = '<div id="q' + qnum + '" class="question"><hr /><h4>Static Text -  #<span id="span' + qnum + '">' + qnum + '</span></h4><input type="hidden" id="ddl"  name="ddl" value="static" /><textarea id="tb' + qnum + '" " cols="40" rows="5" placeholder="Untitled Static Text" class="form-control"></textarea><span id="del' + qnum + '" onclick="delQ(' + qnum + ')" class="btn btn-danger">Delete this Static Text</span><hr /></div> ';
                    $("#totalQ").val(qnum);
                    $("#create").append(tDiv);

                });


                $("#addBtn").click(function () {
                    qnum = parseInt($("#totalQ").val()) + 1;
                    let qDiv = '<div id="q' + qnum + '" class="question"><hr /><h4>Question #<span id="span' + qnum + '">' + qnum + '</span></h4><input type="text" id="tb' + qnum + '"  name="tb' + qnum + '" placeholder="Untitled Question" class="form-control" style="width:40%;" required><span id="del' + qnum + '" onclick="delQ(' + qnum + ')" class="btn btn-danger">Delete this Question</span><br /><label>What is the input type? :</label><br /><select id="ddl' + qnum + '"  name="ddl' + qnum + '" onchange="ddlChange(' + qnum + ')"><option value="short">Short Text</option><option value="long">Long Text</option><option value="multiple">Multiple Choice</option><option value="checkbox">Checkboxes</option><option value="datetime">Date & Time</option></select></div>';
                    $("#totalQ").val(qnum);
                    $("#create").append(qDiv);

                });

                function delQ(qnum) {

                    $('#q' + qnum).remove();
                    reDraw(qnum);
                    $("#totalQ").val(parseInt($("#totalQ").val()) - 1);
                }

                function reDraw(hold) {
                    let qnum = parseInt(hold);
                    let totalNum = parseInt($("#totalQ").val());
                    let correctNum = qnum++;
                    while (qnum <= totalNum) {
                        $('#q' + qnum).attr('name', "q" + correctNum);
                        $('#q' + qnum).attr('id', "q" + correctNum);
                       
                        $('#tb' + qnum).attr('name', "tb" + correctNum);
                        $('#tb' + qnum).attr('id', "tb" + correctNum);
                        

                        $('#span' + qnum).html(correctNum);
                        $('#span' + qnum).attr('id', 'span' + correctNum);



                        $('#del' + qnum).attr('onclick', "delQ(" + correctNum + ")");
                        $('#del' + qnum).attr('id', "del" + correctNum);
                        $('#ddl' + qnum).attr('onchange', "ddlChange(" + correctNum + ")");
                        //     $('#ddl' + qnum).attr('onfocus', "ddlSave(" + correctNum + ")");
                  
                        $('#ddl' + qnum).attr('name', "ddl" + correctNum);
                        $('#ddl' + qnum).attr('id', "ddl" + correctNum);
                        let opTotal = parseInt($('#q' + qnum + 'OptionsTotal').val());
                        let opCount = 1;
                        while (opCount <= opTotal) {
                            $('div#options' + qnum).find('span#delOption' + opCount).attr('onclick', 'delOption(' + correctNum + ', ' + opCount + ')');
                            $('div#options' + qnum).find($('span#delOption' + opCount)).attr('id', 'delOption' + opCount);
                            $('div#options' + qnum).find($('input#q' + qnum + 'Option' + opCount)).attr('name', 'q' + correctNum + 'Option' + opCount);
                            $('div#options' + qnum).find($('input#q' + qnum + 'Option' + opCount)).attr('id', 'q' + correctNum + 'Option' + opCount);
                            opCount++;
                        }

                        $('span#addOption' + qnum).attr('onclick', "addOption(" + correctNum + ")");
                        $('#q' + qnum + "OptionsTotal").attr('id', "q" + correctNum + "OptionsTotal");
                        $('#options' + qnum).attr('id', "options" + correctNum);

                        qnum++;
                        correctNum++;
                    }

                }


                function ddlChange(hold) {
                    if ($('#ddl' + hold).val() == "multiple" || $('#ddl' + hold).val() == "checkbox") {
                        if ($('#options' + hold).length == 0) {
                            drawOption(hold);
                        }
                    }
                    else {
                        if ($('#options' + hold).length > 0) {
                            destroyOption(hold);
                        }
                    }
                }


                /*

                function ddlChange(hold) {
                    if ($('#ddl' + hold).val() == "multiple" || $('#ddl' + hold).val() == "checkbox") {
                        if (ddlMem != "multiple" && ddlMem != "checkbox") {
                            drawOption(hold);
                        }

                    }
                    else if ($('#ddl' + hold).val() != "multiple" && $('#ddl' + hold).val() != "checkbox") {
                        if (ddlMem == "multiple" || ddlMem == "checkbox") {
                            destroyOption(hold);
                        }
                    }


                }
                
                function ddlSave(hold) {
                    ddlMem = $('#ddl' + hold).val();
                }
                */

                function drawOption(hold) {
                    let draw = '<div id="options' + hold + '"><br /><input type="hidden" id="q' + hold + 'OptionsTotal"  name="q' + hold + 'OptionsTotal"value="2" /><input type="text" class="form-control" placeholder="Untitled Option" id="q' + hold + 'Option1" name="q' + hold + 'Option1" required/><span id="delOption1" onclick="delOption(' + hold + ', 1)"  class="btn btn-danger">Delete this option </span><br /> <input type="text" class="form-control" placeholder="Untitled Option" id="q' + hold + 'Option2" name="q' + hold + 'Option2" required><span id="delOption2" onclick="delOption(' + hold + ', 2)" class="btn btn-danger" >Delete this option </span><br /><span id="addOption' + hold + '" onclick="addOption(' + hold + ')" class="btn btn-primary" >Add option</span></div> ';
                    $('#q' + hold).append(draw);
                }

                function addOption(hold) {
                    let opTotal = parseInt($('#q' + hold + 'OptionsTotal').val());
                    opTotal++;
                    let draw = '<input type="text" class="form-control" placeholder="Untitled Option" id="q' + hold + 'Option' + opTotal + '"  name="q' + hold + 'Option' + opTotal + '" /><span id="delOption' + opTotal + '" onclick="delOption(' + hold + ', ' + opTotal + ')" class="btn btn-danger" required >Delete this option </span><br />';
                    $('#q' + hold + 'OptionsTotal').val(opTotal);
                    $('#addOption' + hold).before(draw);
                    
              
                }

                function destroyOption(hold) {
                    $('#options' + hold).remove();
                }



                




                function delOption(qnum, onum) {
                    let totalOp = parseInt($('#q' + qnum + 'OptionsTotal').val());
                    console.log(totalOp + " parsed Total");
                    console.log("destroyed question#" + qnum + "  - option#" + onum);
                    $('#options' + qnum).find('#q' +qnum+ 'Option' + onum).remove();
                    $('#options' + qnum).find('#delOption' + onum).remove();
                    
                    if ($('#options' + qnum).find(":text").length == 0) {
                        destroyOption(qnum);
                        $('#ddl' + qnum).val("short")
                    }
                       

                    else {
                        let correctNum = onum - 1;
                        console.log(qnum + "question number");
                        while (onum <= totalOp) {
                          

                        

                            $('div#options' + qnum).find($('input#q'+qnum+'Option' + onum)).attr('name', 'q'+qnum+'Option' + correctNum);
                            $('div#options' + qnum).find($('input#q' + qnum + 'Option' + onum)).attr('id', 'q'+qnum+'Option' + correctNum);
                            $('div#options' + qnum).find($('span#delOption' + onum)).attr('onclick', 'delOption(' + qnum + ', ' + correctNum + ')');
                            $('div#options' + qnum).find($('span#delOption' + onum)).attr('id', 'delOption' + correctNum);
                            onum++;
                            correctNum++;
                            

                        }
                        totalOp = totalOp - 1;
                        $('#q' + qnum + 'OptionsTotal').val(totalOp);
                    }
                        /*

                    else {
                        onum = parseInt(onum);
                        let correctNum = onum-1;
                        while (onum <= totalOp) {
                           
                            $('#options' + qnum).find($('#Option' + correctNum)).attr('name', 'Option' + onum);
                            $('#options' + qnum).find($('#Option' + correctNum)).attr('id', 'Option' + onum);
                            $('#options' + qnum).find($('#delOption' + correctNum)).attr('onclick', 'delOption(' + qnum + ', ' + onum + ')');
                            $('#options' + qnum).find($('#delOption' + correctNum)).attr('id', 'delOption' + onum);
                        
                            onum++;
                            correctNum++;
                        }
                    }
                    totalOp = totalOp-1;
                    $('#q' + qnum + 'OptionsTotal').val(totalOp);
                    */
                }

                /*
                function reDraw(qnum) {
                    while (qnum < parseInt($("#totalQ").val())) {
                        let pnum = qnum++;
                        $("#q" + pnum).attr("id", "q" + pnum);
                        $("#tb" + pnum).attr("id", "tb" + pnum);
                        $("#tb" + pnum).attr("placeholder", "Untitled Question " + pnum);
                        $("#del" + pnum).attr("onclick", "delQ(" + pnum + ")");
                        $("#ddl" + pnum).attr("onchange", "ddlChange(" + pnum + ")");
                        qnum++;
                    }
                    $("#totalQ").val(parseInt($("#totalQ").val())-1);
                }
                */

            </script>

            </form></div>


</asp:Content>

